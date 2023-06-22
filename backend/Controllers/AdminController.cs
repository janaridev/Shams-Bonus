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

    [HttpPut("{phoneNumber}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateBonuses(string phoneNumber, [FromBody] BonusDto bonuses)
    {
        var leftBonuses = await _adminRepository.BonusDeduction(phoneNumber, bonuses.Value);
        return Ok(leftBonuses);
    }
}