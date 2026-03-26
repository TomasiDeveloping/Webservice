using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Webservice.Infrastructure.Logging.Middleware;

public class TelemetryMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var activity = new Activity("HTTP Request");
        activity.Start();

        await next(context);

        activity.Stop();
    }
}