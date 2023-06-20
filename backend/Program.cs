using backend.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    // Custom Configurations
    builder.Services.ConfigureCors();
    builder.Services.ConfigureSqlContext(builder.Configuration, builder.Environment);
    builder.Services.ConfigureIdentity();

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
