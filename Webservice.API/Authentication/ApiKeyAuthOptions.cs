using Microsoft.AspNetCore.Authentication;
using Webservice.Shared.Constants;

namespace Webservice.API.Authentication;

public class ApiKeyAuthOptions : AuthenticationSchemeOptions
{
    public string HeaderName { get; set; } = HeaderNames.ApiKey;
}