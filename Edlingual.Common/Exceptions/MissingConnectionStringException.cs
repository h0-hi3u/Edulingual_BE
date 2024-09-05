namespace Edulingual.Common.Exceptions;

public class MissingConnectionStringException : ArgumentNullException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public MissingConnectionStringException(string customMessage)
    {
        _customMessage = customMessage;
    }

    public MissingConnectionStringException()
    {
        _customMessage = "Can not find connection string";
    }
}
