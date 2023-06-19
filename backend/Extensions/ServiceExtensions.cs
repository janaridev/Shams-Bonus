using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseMySql(configuration.GetConnectionString("mySqlConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("mySqlConnection"))
            ));
}