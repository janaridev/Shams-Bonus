namespace backend.Data.Repository.Admin;

public interface IAdminRepository
{
    Task<decimal> BonusDeduction(string phoneNumber, decimal bonusDeduction);
    Task<decimal> BonusCalculationsBasedOnCheckAmount(string phoneNumber, decimal checkAmount);
}