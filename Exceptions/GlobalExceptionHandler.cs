using Microsoft.AspNetCore.Diagnostics;
namespace StudyTracker.Api.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
    {
        _logger.LogError(
    exception,
    "Unhandled exception while processing {Method} {Path}",
    httpContext.Request.Method,
    httpContext.Request.Path);
        int statusCode = GetStatusCode(exception);
        string detail = statusCode == StatusCodes.Status500InternalServerError
    ? "An unexpected error occurred."
    : exception.Message;
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                error = detail
            },
        cancellationToken);
        return true;
    }
    public static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            KeyNotFoundException =>
                StatusCodes.Status404NotFound,

            ArgumentException =>
                StatusCodes.Status400BadRequest,

            _ =>
                StatusCodes.Status500InternalServerError
        };
    }
}
