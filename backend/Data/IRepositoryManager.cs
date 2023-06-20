using backend.Data.Repository.Auth;

namespace backend.Data;

public interface IRepositoryManager
{
    IAuthenticationRepository AuthService { get; }
}