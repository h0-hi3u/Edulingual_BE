namespace Edulingual.Common.Exceptions;

public class MissingJwtSettingsException : ArgumentNullException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public MissingJwtSettingsException(string customMessage)
    {
        _customMessage = customMessage;
    }

    public MissingJwtSettingsException()
    {
        _customMessage = "Can not find Jwt configuration!";
    }
}
