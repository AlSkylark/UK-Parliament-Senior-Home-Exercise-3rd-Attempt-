using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Data.ViewModels;
using UKParliament.CodeTest.Services.Services.Interfaces;

namespace UKParliament.CodeTest.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class LookUpController(
    ILookUpService lookUpService,
    IValidator<Department> departmentValidator,
    IValidator<PayBand> payBandValidator
) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public ActionResult<IEnumerable<string>> GetItemsToLookFor()
    {
        return Ok(lookUpService.GetLookupItems());
    }

    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<LookupItem>> Search([FromQuery] LookupItemsEnum? item)
    {
        if (item is null)
        {
            return BadRequest("Lookup item cannot be null");
        }

        return Ok(lookUpService.LookupItem(item));
    }

    [HttpGet("[action]")]
    public ActionResult<IEnumerable<Department>> Departments()
    {
        return Ok(lookUpService.SearchDepartments(null, true));
    }

    [HttpGet("[action]")]
    public ActionResult<IEnumerable<PayBand>> PayBands()
    {
        return Ok(lookUpService.SearchPayBands(null, true));
    }

    [HttpPost("departments")]
    public async Task<ActionResult<Department>> AddDepartment([FromBody] Department update)
    {
        var validation = await departmentValidator.ValidateAsync(update);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        return Ok(lookUpService.EditDepartment(0, update));
    }

    [HttpPost("paybands")]
    public async Task<ActionResult<PayBand>> AddPayBand([FromBody] PayBand update)
    {
        var validation = await payBandValidator.ValidateAsync(update);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        return Ok(lookUpService.EditPayBand(0, update));
    }

    [HttpPut("departments/{id:int}")]
    public async Task<ActionResult<Department>> EditDepartment(
        [FromRoute] int id,
        [FromBody] Department update
    )
    {
        var validation = await departmentValidator.ValidateAsync(update);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        return Ok(lookUpService.EditDepartment(id, update));
    }

    [HttpPut("paybands/{id:int}")]
    public async Task<ActionResult<PayBand>> EditPayBand(
        [FromRoute] int id,
        [FromBody] PayBand update
    )
    {
        var validation = await payBandValidator.ValidateAsync(update);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        return Ok(lookUpService.EditPayBand(id, update));
    }
}
