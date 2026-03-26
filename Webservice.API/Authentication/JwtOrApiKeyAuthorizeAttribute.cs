using Microsoft.AspNetCore.Authorization;

namespace Webservice.API.Authentication;

public class JwtOrApiKeyAuthorizeAttribute : AuthorizeAttribute
{
    public JwtOrApiKeyAuthorizeAttribute() => Policy = AuthPolicies.JwtOrApiKey;
}