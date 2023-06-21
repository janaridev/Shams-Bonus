using backend.Data.Repository.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetBonuses()
    {
        var userId = _clientRepository.GetUserId();
        var bonuses = await _clientRepository.GetBonuses(userId);

        return Ok(bonuses);
    }
}