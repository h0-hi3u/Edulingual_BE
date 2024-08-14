namespace Edulingual.Api.Exceptions;

public class MissingCorsSettingsException : ArgumentNullException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public MissingCorsSettingsException(string customMessage)
    {
        _customMessage = customMessage;
    }
    public MissingCorsSettingsException() 
    {
        _customMessage = "Can not find Cors settings!";
    }
}
