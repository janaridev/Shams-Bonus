using System.Text;
using backend.Application.IServices;
using backend.Application.Services;
using backend.Domain.Entities;
using backend.Domain.IRepository;
using backend.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Api.Extensions;

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

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IAdminService, AdminService>();
    }

    public static void ConfigureRepositoryBase(this IServiceCollection services) =>
        services.AddScoped<IRepositoryBase, RepositoryBase>();

    public static void ConfgigureHostEnvironment(this IServiceCollection services) =>
        services.AddSingleton<IWebHostEnvironment>();

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

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = Environment.GetEnvironmentVariable("SECRET");

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }
}