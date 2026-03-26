using Microsoft.AspNetCore.Authorization;

namespace Webservice.API.Authentication;

public class JwtAuthorizeAttribute : AuthorizeAttribute
{
    public JwtAuthorizeAttribute() => Policy = AuthPolicies.JwtOnly;
}