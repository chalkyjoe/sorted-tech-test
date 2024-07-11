using RainfallApi.Models;
using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception has been caught by the middleware.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        if (exception is HttpStatusCodeException statusCodeException)
        {
            statusCode = statusCodeException.StatusCode;
        }

        var propertyName = "Exception";
        if (exception is PropertyException controllerException)
        {
            propertyName = controllerException.PropertyName;
        }

        var errorResponse = new ErrorResponse
        {
            Message = "An unexpected error has occurred.",
            Detail = new List<ErrorDetail>
            {
                new ErrorDetail
                {
                    PropertyName = propertyName,
                    Message = exception.Message
                }
            }
        };

        var result = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}