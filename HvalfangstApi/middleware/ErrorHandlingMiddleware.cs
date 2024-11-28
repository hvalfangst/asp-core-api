using System.Net;
using System.Text.Json;

namespace HvalfangstApi.middleware;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.Conflict;  //GetStatusCodeFromException(exception);
        context.Response.StatusCode = (int)statusCode;
        var errorObject = new { error = exception.Message, code = statusCode };
        var errorMessage = JsonSerializer.Serialize(errorObject);
        return context.Response.WriteAsync(errorMessage!);
    }
}