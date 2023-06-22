namespace backend.Data.Repository.Admin;

public interface IAdminRepository
{
    Task<decimal> BonusDeduction(string phoneNumber, decimal bonusDeduction);
}