using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Data.Repositories.Interfaces;
using UKParliament.CodeTest.Data.ViewModels;
using UKParliament.CodeTest.Services.Helpers;
using UKParliament.CodeTest.Services.Mappers.Interfaces;
using UKParliament.CodeTest.Services.Services.Interfaces;

namespace UKParliament.CodeTest.Services.Services;

public class LookUpService(
    ILookupRepository<Department> departmentRepo,
    ILookupRepository<PayBand> payBandRepo,
    IEmployeeRepository employeeRepo,
    ILookupMapper mapper
) : ILookUpService
{
    public async Task<Department?> GetDepartment(int id)
    {
        return await departmentRepo.GetById(id);
    }

    public async Task<PayBand?> GetPayBand(int id)
    {
        return await payBandRepo.GetById(id);
    }

    public IEnumerable<Department> SearchDepartments(string? name, bool orderById = false)
    {
        return SearchLookupItem(name, departmentRepo, orderById);
    }

    public IEnumerable<PayBand> SearchPayBands(string? name, bool orderById = false)
    {
        return SearchLookupItem(name, payBandRepo, orderById);
    }

    public async Task<Department> EditDepartment(int id, Department update)
    {
        return await EditLookupItem(id, update, departmentRepo);
    }

    public async Task<PayBand> EditPayBand(int id, PayBand update)
    {
        return await EditLookupItem(id, update, payBandRepo);
    }

    public IEnumerable<string> GetLookupItems()
    {
        return EnumHelper.GetNameValues(typeof(LookupItemsEnum));
    }

    public IEnumerable<LookupItem> LookupItem(LookupItemsEnum? item)
    {
        return item switch
        {
            LookupItemsEnum.Department => SearchLookupItem(null, departmentRepo)
                .Select(mapper.MapToSimple),
            LookupItemsEnum.PayBand => SearchLookupItem(null, payBandRepo)
                .Select(mapper.MapToSimple),
            LookupItemsEnum.EmployeeType => EnumHelper
                .GetNameValues(typeof(EmployeeTypeEnum))
                .Select(mapper.MapFromString)
                .OrderBy(i => i.Name),
            LookupItemsEnum.Manager => employeeRepo
                .Search()
                .Where(e => e.EmployeeType == EmployeeTypeEnum.Manager)
                .Select(mapper.MapFromEmployee),
            null => [],
            _ => [],
        };
    }

    private static IEnumerable<T> SearchLookupItem<T>(
        string? name,
        ILookupRepository<T> repo,
        bool orderById = false
    )
        where T : BaseEntity, ILookupItem
    {
        var items = repo.Search();

        if (name is not null)
        {
            items = items.Where(d =>
                string.Equals(d.Name, name, StringComparison.CurrentCultureIgnoreCase)
            );
        }

        if (!orderById)
        {
            items = items.OrderBy(i => i.Name);
        }
        else
        {
            items = items.OrderBy(i => i.Id);
        }

        return items.AsEnumerable();
    }

    private static async Task<T> EditLookupItem<T>(int id, T update, ILookupRepository<T> repo)
        where T : BaseEntity, ILookupItem
    {
        if (id == 0)
        {
            return await repo.Create(update);
        }

        return await repo.Update(update);
    }
}
