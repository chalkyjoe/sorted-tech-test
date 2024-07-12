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
        var exceptionMatrix = new Dictionary<Type, Func<Exception, Task>>
        {
            [typeof(NotFoundException)] = m => WriteException(context, m, "", HttpStatusCode.NotFound),
            [typeof(ValidationException)] = m => WriteException(context, m, ((ValidationException)m).PropertyName, HttpStatusCode.BadRequest),
            [typeof(ApiException)] = m => WriteException(context, m, "", HttpStatusCode.InternalServerError)
        };
        if (exceptionMatrix.TryGetValue(exception.GetType(), out var action))
        {
            return action(exception);
        }

        throw exception;
    }

    private static Task WriteException( HttpContext context, Exception exception, string propertyName,
        HttpStatusCode statusCode )
    {
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