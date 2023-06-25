using backend.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.IServices;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
}