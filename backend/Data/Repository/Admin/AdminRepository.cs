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

    public async Task<decimal> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        user.Bonuses += CalculateBonuses(checkAmount, user.Bonuses);

        await _context.SaveChangesAsync();
        return decimal.Round(user.Bonuses);
    }

    private decimal CalculateBonuses(decimal checkAmount, decimal currentBonuses)
    {
        if (checkAmount > 0)
        {
            if (checkAmount >= 10000 && checkAmount <= 50000)
            {
                currentBonuses = checkAmount * 0.05m; // 5% bonus
            }
            else if (checkAmount > 50000)
            {
                currentBonuses = checkAmount * 0.1m; // 10% bonus
            }
        }

        return currentBonuses;
    }
}