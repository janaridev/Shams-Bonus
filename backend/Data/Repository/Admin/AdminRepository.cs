using backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Repository.Admin;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RepositoryContext _context;

    public AdminRepository(UserManager<User> userManager, RepositoryContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<decimal> BonusDeduction(string phoneNumber, decimal bonusDeduction)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);

        if (user.Bonuses >= bonusDeduction)
        {
            user.Bonuses -= bonusDeduction;
            await _context.SaveChangesAsync();
        }

        return decimal.Round(user.Bonuses);
    }
}