using backend.Presentation.ActionFilters;
using backend.Application.Dtos;
using backend.Application.IServices;
using Microsoft.AspNetCore.Mvc;

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
        if (result == null) return BadRequest("Пароли не совпадают.");

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await _authService.ValidateUser(user))
            return Unauthorized();

        return Ok(new { Token = await _authService.CreateToken() });
    }
}