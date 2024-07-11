using System.Net;

namespace Api.Exceptions;

public class HttpStatusCodeException (HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "") : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}