using System.Net;
using System.Text.Json;
using Hvalfangst.api.exception;

namespace Hvalfangst.api.middleware;

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
        var statusCode = GetStatusCodeFromException(exception);
        context.Response.StatusCode = statusCode;
        var errorObject = new { error = exception.Message, code = statusCode };
        var errorMessage = JsonSerializer.Serialize(errorObject);
        return context.Response.WriteAsync(errorMessage!);
    }

    private static int GetStatusCodeFromException(Exception exception)
    {
        FireballException? fireballException = null;
        SpellDodgeException? spellDodgeException = null;

        switch (exception)
        {
            case FireballException fbException:
                fireballException = fbException;
                break;
            case SpellDodgeException sdException:
                spellDodgeException = sdException;
                break;
            default:
                return (int)HttpStatusCode.InternalServerError;
        }

        if (fireballException != null)
        {
            if (fireballException.Message.Contains("Big ball of fire"))
            {
                return (int)HttpStatusCode.VariantAlsoNegotiates;
            }

            if (fireballException.Message.Contains("No fiery ball to be seen"))
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.Conflict;
        }

        if (spellDodgeException == null)
            return (int)HttpStatusCode.InternalServerError;
    
        if (spellDodgeException.Message.Contains("Dodged spell like a pandaren brewmaster"))
        {
            return (int)HttpStatusCode.PaymentRequired; 
        }
        
        return (int)HttpStatusCode.Conflict; 
    }
}