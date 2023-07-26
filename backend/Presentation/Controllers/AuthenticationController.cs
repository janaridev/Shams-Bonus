using backend.Presentation.ActionFilters;
using backend.Application.Dtos;
using backend.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await _authService.RegisterUser(userForRegistration);
        if (result.StatusCode == HttpStatusCode.Conflict) { return Conflict(result); }

        return result.StatusCode is HttpStatusCode.Created ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        var result = await _authService.ValidateUser(user);
        return result.StatusCode is HttpStatusCode.OK ? Ok(result) : BadRequest(result);
    }
}