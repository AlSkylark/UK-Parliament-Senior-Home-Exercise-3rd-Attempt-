using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Data.ViewModels;

namespace UKParliament.CodeTest.Services.Services.Interfaces;

public interface ILookUpService
{
    Task<Department?> GetDepartment(int id);
    Task<PayBand?> GetPayBand(int id);

    IEnumerable<Department> SearchDepartments(string? name, bool orderById = false);
    IEnumerable<PayBand> SearchPayBands(string? name, bool orderById = false);

    Task<Department> EditDepartment(int id, Department update);
    Task<PayBand> EditPayBand(int id, PayBand update);

    IEnumerable<LookupItem> LookupItem(LookupItemsEnum? item);
    IEnumerable<string> GetLookupItems();
}
