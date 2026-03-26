using Webservice.Application.Models;

namespace Webservice.Application.Security;

public interface IApiKeyValidator
{
    Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken ct);
}