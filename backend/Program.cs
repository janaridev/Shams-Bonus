using backend.ActionFilters;
using backend.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
{
    // Custom Configurations
    builder.Services.ConfigureCors();
    builder.Services.ConfigureSqlContext(builder.Configuration, builder.Environment);
    builder.Services.ConfigureServices();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJWT(builder.Configuration);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddAuthentication();
    builder.Services.AddScoped<ValidationFilterAttribute>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseCors("CorsPolicy");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
