namespace Edulingual.Service.Response.Payment;

public class ViewPaymentResponse
{
    public double Fee { get; set; } = 0;
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
