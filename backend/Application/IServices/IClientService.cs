using backend.Domain.Entities;

namespace backend.Application.IServices;

public interface IClientService
{
    string GetUserId();
    Task<ApiResponse> GetBonuses(string userId);
}