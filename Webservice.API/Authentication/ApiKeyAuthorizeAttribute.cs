using Microsoft.AspNetCore.Authorization;

namespace Webservice.API.Authentication;

public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
{
    public ApiKeyAuthorizeAttribute() => Policy = AuthPolicies.ApiKeyOnly;
}