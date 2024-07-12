using System.Net;

namespace Domain.Exceptions;

public class ValidationException(string propertyName, string message = "") : Exception(message)
{
    public string PropertyName { get; } = propertyName;
}