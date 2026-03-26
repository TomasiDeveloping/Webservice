using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.API.Middleware;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title, detail) = exception switch
        {
            // Handle specific exceptions here and set appropriate status, title, and detail
            ArgumentException argEx => (
                StatusCodes.Status400BadRequest,
                "Invalid request.",
                argEx.Message),
            _ => (StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                "An unexpected error occurred while processing your request. Please try again later.")
        };

        if (status == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "An unexpected error occurred while processing the request.");
        }
        else
        {
            logger.LogWarning(exception, "A handled exception occurred: {Message}", exception.Message);
        }

        httpContext.Response.StatusCode = status;

        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            }
        });
    }
}