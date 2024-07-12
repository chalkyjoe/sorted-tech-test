namespace Dtos.Error;

public class ErrorResponse
{
    public string Message { get; set; }
    public List<ErrorDetail> Detail { get; set; }
}