using backend.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetBonuses()
    {
        var userId = _clientService.GetUserId();
        var bonuses = await _clientService.GetBonuses(userId);

        return Ok(bonuses);
    }
}