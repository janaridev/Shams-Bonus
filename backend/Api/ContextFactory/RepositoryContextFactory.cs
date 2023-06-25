using backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Api.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENV");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var mySqlConnection = env == "Development" ?
            configuration.GetConnectionString("mySqlConnectionDevelopment") :
            Environment.GetEnvironmentVariable("mySqlConnectionProduction");

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                b => b.MigrationsAssembly("Api"));

        return new RepositoryContext(builder.Options);
    }
}