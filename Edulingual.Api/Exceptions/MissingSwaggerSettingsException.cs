namespace Edulingual.Api.Exceptions;

public class MissingSwaggerSettingsException : ArgumentNullException
{
    private readonly string? _customMessage;

    public override string Message => _customMessage ?? Message;

    public MissingSwaggerSettingsException(string customMessage)
    {
        _customMessage = customMessage;
    }

    public MissingSwaggerSettingsException()
    {
        _customMessage = "Can not find swagger config";
    }
}
