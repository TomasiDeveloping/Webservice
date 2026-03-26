namespace Webservice.API.Authentication;

public static class AuthPolicies
{
    public const string JwtOnly = "JwtOnly";

    public const string ApiKeyOnly = "ApiKeyOnly";

    public const string JwtOrApiKey = "JwtOrApiKey";
}