using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Webservice.Application.Security;

namespace Webservice.API.Authentication;

public sealed class ApiKeyAuthHandler(
    IOptionsMonitor<ApiKeyAuthOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IApiKeyValidator validator,
    IProblemDetailsService problemDetailsService)
    : AuthenticationHandler<ApiKeyAuthOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.HeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        var key = apiKeyHeaderValues.ToString();
        var result = await validator.ValidateAsync(key, Context.RequestAborted);

        if (!result.IsValid)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, result.ClientId ?? string.Empty)
        };

        claims.AddRange(result.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);

        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (Context.Response.HasStarted) return;
        
        Response.StatusCode = StatusCodes.Status401Unauthorized;

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = Context,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "API key is missing or invalid",
                Instance = Context.Request.Path
            }
        });

    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        if (Context.Response.HasStarted) return;

        Response.StatusCode = StatusCodes.Status403Forbidden;

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = Context,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "You do not have permission to access this resource",
                Instance = Context.Request.Path
            }
        });
    }
}