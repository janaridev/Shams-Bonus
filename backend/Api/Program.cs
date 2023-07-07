using backend.Presentation.ActionFilters;
using backend.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    // Custom Configurations
    builder.Services.ConfigureCors();
    builder.Services.ConfigureSqlContext(builder.Configuration, builder.Environment);
    builder.Services.ConfigureRepositoryBase();
    builder.Services.ConfigureServices();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJWT(builder.Configuration);

    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddScoped<ValidationFilterAttribute>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAuthentication();

    builder.Services.AddControllers()
        .AddApplicationPart(typeof(backend.Presentation.AssemblyReference).Assembly);
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

