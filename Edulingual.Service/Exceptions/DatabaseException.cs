using Edulingual.Common.Exceptions;

namespace Edulingual.Service.Exceptions;

public class DatabaseException : Exception, IDatabaseException
{
    private readonly string _customMessage;

    public override string Message => _customMessage ?? Message;

    public DatabaseException(string customMessage)
    {
        _customMessage = customMessage;
    }

    public DatabaseException()
    {
        _customMessage = "Internal Server Error!";
    }
}
