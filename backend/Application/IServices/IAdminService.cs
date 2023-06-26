namespace backend.Application.IServices;

public interface IAdminService
{
    Task<decimal> BonusDeduction(string phoneNumber, decimal bonusesForDeduction);
    Task<decimal> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount);
}