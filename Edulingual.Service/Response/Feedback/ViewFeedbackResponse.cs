namespace Edulingual.Service.Response.Feedback;

public class ViewFeedbackResponse
{
    public Guid CourseId { get; set; }
    public string? Content { get; set; } = string.Empty;
    public int Rating { get; set; }
}
