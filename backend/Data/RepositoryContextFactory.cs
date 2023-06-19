using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var mySqlConnection = configuration.GetConnectionString("mySqlConnectionDevelopment");

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                b => b.MigrationsAssembly("backend"));

        return new RepositoryContext(builder.Options);
    }
}