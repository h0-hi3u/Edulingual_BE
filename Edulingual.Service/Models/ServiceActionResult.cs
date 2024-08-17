using System.Net;

namespace Edulingual.Service.Models;

public class ServiceActionResult
{
    //public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    //public AppActionResult(bool isSuccess)
    //{
    //    IsSuccess = isSuccess;
    //}
    public ServiceActionResult(object data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        Data = data;
        HttpStatusCode = httpStatusCode;
    }

    //public AppActionResult(bool isSuccess, string message)
    //{
    //    IsSuccess = isSuccess;
    //    Message = message;
    //}
    //public AppActionResult() : this(true) { }
}
