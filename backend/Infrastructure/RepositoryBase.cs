using backend.Domain.IRepository;

namespace backend.Infrastructure;

public class RepositoryBase : IRepositoryBase
{
    protected RepositoryContext _repositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext) => _repositoryContext = repositoryContext;

    public async Task SaveChangesAsync() => await _repositoryContext.SaveChangesAsync();
}