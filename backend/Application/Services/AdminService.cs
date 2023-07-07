using System.Net;
using backend.Application.IServices;
using backend.Domain.Entities;
using backend.Domain.IRepository;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryBase _repositoryBase;

    protected ApiResponse _response;

    public AdminService(UserManager<User> userManager, IRepositoryBase repositoryBase)
    {
        _userManager = userManager;
        _repositoryBase = repositoryBase;
        _response = new();
    }

    public async Task<ApiResponse> BonusDeduction(string phoneNumber, decimal bonusesForDeduction)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.ErrorMessages.Add("Данного номера телефона не существует.");

            return _response;
        };

        if (user.Bonuses < bonusesForDeduction)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add("Недостаточно средств");

            return _response;
        }

        user.Bonuses -= bonusesForDeduction;
        await _repositoryBase.SaveChangesAsync();

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = decimal.Round(user.Bonuses);

        return _response;
    }

    public async Task<ApiResponse> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.ErrorMessages.Add("Данного номера телефона не существует.");

            return _response;
        };

        user.Bonuses += CalculateBonuses(checkAmount);
        await _repositoryBase.SaveChangesAsync();

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = decimal.Round(user.Bonuses);

        return _response;
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