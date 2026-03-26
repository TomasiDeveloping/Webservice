namespace Webservice.Application.Models;

public record ApiKeyValidationResult(bool IsValid, string? ClientId, string[] Roles); 