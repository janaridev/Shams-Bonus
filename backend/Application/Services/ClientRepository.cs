using System.Security.Claims;
using backend.Application.IServices;
using backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.Services;

public class ClientService : IClientService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public ClientService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<decimal> GetBonuses(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return decimal.Round(user.Bonuses);
    }

    public string GetUserId()
    {
        var userId = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
            userId = _httpContextAccessor.HttpContext.User.FindFirstValue("userId");

        return userId;
    }
}