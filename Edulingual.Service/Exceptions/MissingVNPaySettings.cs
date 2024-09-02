namespace Edulingual.Service.Exceptions;

public class MissingVNPaySettings : ArgumentNullException
{
    private readonly string? _customMessage;
    public override string Message => _customMessage ?? Message;

    public MissingVNPaySettings(string customMessage)
    {
        _customMessage = customMessage;
    }
    public MissingVNPaySettings()
    {
        _customMessage = "Not found VNPay settings!";
    }
}
