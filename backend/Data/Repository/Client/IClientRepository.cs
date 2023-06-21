namespace backend.Data.Repository.Client;

public interface IClientRepository
{
    string GetUserId();
    Task<decimal> GetBonuses(string userId);
}