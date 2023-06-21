using System.Security.Claims;
using backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Repository.Client;

public class ClientRepository : IClientRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public ClientRepository(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
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