using backend.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    // Custom Extensions
    builder.Services.ConfigureSqlContext(builder.Configuration);
    builder.Services.ConfigureIdentity();

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
