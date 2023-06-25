namespace backend.Application.IServices;

public interface IClientService
{
    string GetUserId();
    Task<decimal> GetBonuses(string userId);
}