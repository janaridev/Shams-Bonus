using backend.Domain.Entities;

namespace backend.Application.IServices;

public interface IAdminService
{
    Task<ApiResponse> BonusDeduction(string phoneNumber, decimal bonusesForDeduction);
    Task<ApiResponse> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount);
}