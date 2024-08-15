namespace Edulingual.Service.Models;

public class AppActionResult
{
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }

    public AppActionResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public AppActionResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public AppActionResult() : this(true) { }
}
