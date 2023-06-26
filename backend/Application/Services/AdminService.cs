using backend.Application.IServices;
using backend.Domain.Entities;
using backend.Domain.Exceptions.NotFound;
using backend.Domain.IRepository;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryBase _repositoryBase;

    public AdminService(UserManager<User> userManager, IRepositoryBase repositoryBase)
    {
        _userManager = userManager;
        _repositoryBase = repositoryBase;
    }

    public async Task<decimal> BonusDeduction(string phoneNumber, decimal bonusDeduction)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
            throw new UserNotFoundException(phoneNumber);

        if (user.Bonuses >= bonusDeduction)
        {
            user.Bonuses -= bonusDeduction;
            await _repositoryBase.SaveChangesAsync();
        }

        return decimal.Round(user.Bonuses);
    }

    public async Task<decimal> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
            throw new UserNotFoundException(phoneNumber);

        user.Bonuses += CalculateBonuses(checkAmount);

        await _repositoryBase.SaveChangesAsync();
        return decimal.Round(user.Bonuses);
    }

    private decimal CalculateBonuses(decimal checkAmount)
    {
        decimal result = 0;
        if (checkAmount > 0)
        {
            if (checkAmount >= 10000 && checkAmount <= 50000)
            {
                result = checkAmount * 0.05m; // 5% bonus
            }
            else if (checkAmount > 50000)
            {
                result = checkAmount * 0.1m; // 10% bonus
            }
        }

        return result;
    }
}