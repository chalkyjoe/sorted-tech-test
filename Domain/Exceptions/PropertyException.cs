using System.Net;

namespace Domain.Exceptions;

public class PropertyException(string propertyName, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "") : HttpStatusCodeException(statusCode, message)
{
    public string PropertyName { get; } = propertyName;
}