using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Webservice.Infrastructure.Logging.Middleware;

public class LoggingMiddleware: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sw = Stopwatch.StartNew();
        await next(context);
        sw.Stop();

        Log.Information("HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            sw.ElapsedMilliseconds);
    }
}