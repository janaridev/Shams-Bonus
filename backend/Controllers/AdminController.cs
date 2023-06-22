using backend.Data.Repository.Admin;
using backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpPut("deductionBonuses/{phoneNumber}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeductionBonuses(string phoneNumber, [FromBody] BonusDto bonuses)
    {
        var leftBonuses = await _adminRepository.BonusDeduction(phoneNumber, bonuses.Value);
        return Ok(leftBonuses);
    }

    [HttpPut("addBonuses/{phoneNumber}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddBonuses(string phoneNumber, [FromBody] CheckAmountDto checkAmount)
    {
        var currentBonuses = await _adminRepository.BonusCalculationsBasedOnCheckAmount(phoneNumber,
            checkAmount.Value);
        return Ok(currentBonuses);
    }
}