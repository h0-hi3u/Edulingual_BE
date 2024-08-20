namespace Edulingual.Caching.Exceptions;

public class MissingRedisSettingsException : ArgumentNullException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public MissingRedisSettingsException()
    {
        _customMessage = "Not found Redis setting!";
    }
}
