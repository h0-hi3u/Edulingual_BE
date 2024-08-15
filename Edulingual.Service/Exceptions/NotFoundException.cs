using Edulingual.Common.Exceptions;

namespace Edulingual.Service.Exceptions;

public class NotFoundException : NullReferenceException, INotFoundException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public NotFoundException(string customMessage)
    {
        _customMessage = customMessage;
    }
    public NotFoundException()
    {
        _customMessage = "Not found!";
    }
}
