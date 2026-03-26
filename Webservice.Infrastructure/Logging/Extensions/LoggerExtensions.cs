

using Serilog;

namespace Webservice.Infrastructure.Logging.Extensions;

public static class LoggerExtensions
{
    extension(ILogger logger)
    {
        public void LogClientAction(string clientName, string action)
        {
            logger.Information("Client '{ClientName}' performed action: {Action}", clientName, action);
        }

        public void LogApiKeyUsage(string apiKey, string endpoint)
        {
            logger.Information("API key '{ApiKey}' used to access endpoint: {Endpoint}", apiKey, endpoint);
        }
    }

}