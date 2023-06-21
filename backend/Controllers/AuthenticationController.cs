using backend.ActionFilters;
using backend.Data;
using backend.Data.Repository.Auth;
using backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationRepository _authRepository;

    public AuthenticationController(IAuthenticationRepository authentRepository)
    {
        _authRepository = authentRepository;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await _authRepository.RegisterUser(userForRegistration);
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
        if (!await _authRepository.ValidateUser(user))
            return Unauthorized();

        return Ok(new { Token = await _authRepository.CreateToken() });
    }
}