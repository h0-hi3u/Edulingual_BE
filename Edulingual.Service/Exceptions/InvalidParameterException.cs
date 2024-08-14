using Edulingual.Common.Exceptions;

namespace Edulingual.Service.Exceptions;

public class InvalidParameterException : ArgumentNullException, IInvalidException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public InvalidParameterException(string customMessage)
    {
        _customMessage = customMessage;
    }

    public InvalidParameterException()
    {
        _customMessage = "Invalid parameter!";
    }
}
