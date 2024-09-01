namespace Edulingual.Service.Library;

public class VNPayModel
{
    public string TmnCode { get; set; }
    public string HashSecret { get; set; }
    public string Url { get; set; }
    public string ReturnUrl { get; set; }
    public string CancelUrl { get; set; }
}
