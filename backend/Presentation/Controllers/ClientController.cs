using System.Net;
using backend.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/user")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetBonuses()
    {
        var userId = _clientService.GetUserId();
        var result = await _clientService.GetBonuses(userId);

        return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode(500, result);
    }
}