using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Webservice.API.Authentication;
using Webservice.Shared.Constants;

namespace Webservice.API.Extensions;

public static class AuthExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAndConfigureAuthentication(IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = AuthSchemes.Smart;
                    options.DefaultChallengeScheme = AuthSchemes.Smart;
                })
                .AddPolicyScheme(AuthSchemes.Smart, "JWT or API Key", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var authHeader = context.Request.Headers.Authorization.ToString();
                        if (!string.IsNullOrWhiteSpace(authHeader) &&
                            authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            return AuthSchemes.Jwt;
                        }

                        return context.Request.Headers.ContainsKey(HeaderNames.ApiKey) ? AuthSchemes.ApiKey : AuthSchemes.Jwt;
                    };
                })
                .AddJwtBearer(AuthSchemes.Jwt, options =>
                {
                    // TODO:
                    options.Audience = "api";

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            if (context.Response.HasStarted)
                            {
                                return;
                            }
                            var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                            // Skip the default logic.
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                            {
                                HttpContext = context.HttpContext,
                                ProblemDetails = new ProblemDetails
                                {
                                    Status = StatusCodes.Status401Unauthorized,
                                    Title = "Unauthorized",
                                    Detail = "JWT token is missing or invalid",
                                    Instance = context.HttpContext.Request.Path
                                }
                            });
                        },
                        OnForbidden = async context =>
                        {
                            if (context.Response.HasStarted)
                            {
                                return;
                            }
                            var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                            {
                                HttpContext = context.HttpContext,
                                ProblemDetails = new ProblemDetails
                                {
                                    Status = StatusCodes.Status403Forbidden,
                                    Title = "Forbidden",
                                    Detail = "You do not have permission to access this resource",
                                    Instance = context.HttpContext.Request.Path
                                }
                            });
                        }
                    };
                })
                .AddScheme<ApiKeyAuthOptions, ApiKeyAuthHandler>(AuthSchemes.ApiKey, options =>
                {
                    options.HeaderName = HeaderNames.ApiKey;
                });
            return services;
        }

        public IServiceCollection AddAndConfigureAuthorization()
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(AuthPolicies.JwtOnly, p =>
                {
                    p.AddAuthenticationSchemes(AuthSchemes.Jwt).RequireAuthenticatedUser();
                })
                .AddPolicy(AuthPolicies.ApiKeyOnly, p =>
                {
                    p.AddAuthenticationSchemes(AuthSchemes.ApiKey).RequireAuthenticatedUser();
                })
                .AddPolicy(AuthPolicies.JwtOrApiKey, p =>
                {
                    p.AddAuthenticationSchemes(AuthSchemes.Jwt,AuthSchemes.Jwt).RequireAuthenticatedUser();
                });

            return services;
        }
    }
}