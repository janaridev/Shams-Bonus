namespace backend.Domain.IRepository;

public interface IRepositoryBase
{
    Task SaveChangesAsync();
}