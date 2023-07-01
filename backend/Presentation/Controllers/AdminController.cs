using backend.Presentation.ActionFilters;
using backend.Application.Dtos;
using backend.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPut("deductionBonuses/{phoneNumber}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeductionBonuses(string phoneNumber, [FromBody] BonusDto bonuses)
    {
        var result = await _adminService.BonusDeduction(phoneNumber, bonuses.Value);
        return result.StatusCode == HttpStatusCode.OK ? Ok(result) : NotFound(result);
    }

    [HttpPut("addBonuses/{phoneNumber}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddBonuses(string phoneNumber, [FromBody] CheckAmountDto checkAmount)
    {
        var result = await _adminService.BonusCalculationsBasedOnCheckAmount(phoneNumber,
            checkAmount.Value);
        return result.StatusCode == HttpStatusCode.OK ? Ok(result) : NotFound(result);
    }
}