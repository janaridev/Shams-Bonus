using backend.Application.Dtos;
using backend.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeductionBonuses(string phoneNumber, [FromBody] BonusDto bonuses)
    {
        var leftBonuses = await _adminService.BonusDeduction(phoneNumber, bonuses.Value);
        return Ok(leftBonuses);
    }

    [HttpPut("addBonuses/{phoneNumber}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddBonuses(string phoneNumber, [FromBody] CheckAmountDto checkAmount)
    {
        var currentBonuses = await _adminService.BonusCalculationsBasedOnCheckAmount(phoneNumber,
            checkAmount.Value);
        return Ok(currentBonuses);
    }
}