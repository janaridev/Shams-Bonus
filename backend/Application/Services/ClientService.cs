using System.Net;
using System.Security.Claims;
using backend.Application.IServices;
using backend.Domain.Entities;
using backend.Domain.Exceptions.BadRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.Services;

public class ClientService : IClientService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    protected ApiResponse _response;

    public ClientService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _response = new();
    }

    public async Task<ApiResponse> GetBonuses(string userId)
    {
        if (userId is null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add("Что то пошло не так :(");
        }

        var user = await _userManager.FindByIdAsync(userId);

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = decimal.Round(user.Bonuses);

        return _response;
    }

    public string GetUserId()
    {
        var userId = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue("userId");

        if (userId is null)
            throw new Exception("User Id is null");

        return userId;
    }
}