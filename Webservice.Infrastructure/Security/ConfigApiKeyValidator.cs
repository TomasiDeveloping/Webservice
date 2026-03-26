using Webservice.Application.Models;
using Webservice.Application.Security;

namespace Webservice.Infrastructure.Security;

public class ConfigApiKeyValidator : IApiKeyValidator
{
    public Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken ct)
    {
        // TODO: Replace this with Database API key validation logic
        const string expectedApiKey = "TestApiKey123";

        if (!string.IsNullOrWhiteSpace(expectedApiKey) && apiKey == expectedApiKey)
        {
            return Task.FromResult(new ApiKeyValidationResult(true, "TestClient", ["Client"]));
        }

        return Task.FromResult(new ApiKeyValidationResult(false, null, []));
    }
}