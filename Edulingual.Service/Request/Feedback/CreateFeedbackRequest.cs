namespace Edulingual.Service.Request.Feedback;

public class CreateFeedbackRequest
{
    public Guid CourseId { get; set; }
    public string? Content { get; set; } = string.Empty;
    public int Rating { get; set; }
}
