using Microsoft.AspNetCore.Diagnostics;
namespace StudyTracker.Api.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                error = exception.Message
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
