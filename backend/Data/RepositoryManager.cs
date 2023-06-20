using AutoMapper;
using backend.Data.Repository.Auth;
using backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Data;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IAuthenticationRepository> _authRepository;

    public RepositoryManager(IMapper mapper, UserManager<User> userManager,
        IConfiguration configuration)
    {
        _authRepository = new Lazy<IAuthenticationRepository>(() => new
            AuthenticationRepository(mapper, userManager, configuration));
    }


    public IAuthenticationRepository AuthService => _authRepository.Value;
}