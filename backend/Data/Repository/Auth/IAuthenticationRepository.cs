using backend.Dtos;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Repository.Auth;

public interface IAuthenticationRepository
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
}