using System.Net;

namespace Api.Exceptions;

public class ControllerException(string propertyName, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "") : HttpStatusCodeException(statusCode, message)
{
    public string PropertyName { get; } = propertyName;
}