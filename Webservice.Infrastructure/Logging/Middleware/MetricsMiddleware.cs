using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;
using Webservice.Shared.Constants;

namespace Webservice.Infrastructure.Logging.Middleware;

public class MetricsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sw = Stopwatch.StartNew();
        await next(context);
        sw.Stop();

        var apiKeyPrefix = context.Request.Headers.TryGetValue(HeaderNames.ApiKey, out var apiKey)
            ? apiKey.ToString()
            : "Anonymous";


        Log.Information("API Key {ApiKeyPrefix} called {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
            apiKeyPrefix,
            context.Request.Path,
            context.Response.StatusCode,
            sw.ElapsedMilliseconds);
    }
}