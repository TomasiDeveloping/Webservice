using Microsoft.Extensions.Configuration;
using Serilog;

namespace Webservice.Infrastructure.Logging.Configuration;

public static class SerilogConfig
{
    public static void ConfigureSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}