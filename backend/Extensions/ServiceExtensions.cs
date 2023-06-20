using backend.Data;
using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var productionConnection = Environment.GetEnvironmentVariable("mySqlConnectionProduction");

        if (env.IsDevelopment())
        {
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseMySql(configuration.GetConnectionString("mySqlConnectionDevelopment"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("mySqlConnectionDevelopment"))
                ));
            Console.WriteLine("--> Using Development Database");
        }
        else
        {
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseMySql(productionConnection,
                    ServerVersion.AutoDetect(productionConnection)
                ));
            Console.WriteLine("--> Using Production Database");
        }
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 7;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }
}