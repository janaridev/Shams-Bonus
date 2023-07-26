using backend.Application.Dtos;
using backend.Domain.Entities;

namespace backend.Application.IServices;

public interface IAuthenticationService
{
    Task<ApiResponse> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<ApiResponse> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
}