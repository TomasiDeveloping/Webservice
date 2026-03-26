using Serilog;
using Webservice.API.Extensions;
using Webservice.Application;
using Webservice.Application.Security;
using Webservice.Domain;
using Webservice.Infrastructure;
using Webservice.Infrastructure.Logging.Middleware;
using Webservice.Infrastructure.Security;
using Webservice.Infrastructure.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddProblemDetails();

    builder.Services.AddScoped<IApiKeyValidator, ConfigApiKeyValidator>();

    builder.Services.AddAndConfigureAuthentication(builder.Configuration);
    builder.Services.AddAndConfigureAuthorization();

    // Dependency Injection for Layers
    builder.Services.AddDomainDependencies();
    builder.Services.AddApplicationDependencies();
    builder.Services.AddInfrastructureDependencies(builder.Configuration);

    builder.Host.UseSerilog();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }

  
    app.MapOpenApi();
    app.MapScalar();


    // Middleware
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<MetricsMiddleware>();
    app.UseMiddleware<TelemetryMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers();

    app.MapFallbackToFile("index.html");

    Log.Information("Webservice API started successfully");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


