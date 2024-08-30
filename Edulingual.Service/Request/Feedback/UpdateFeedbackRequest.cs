namespace Edulingual.Service.Request.Feedback;

public class UpdateFeedbackRequest
{
    public Guid CourseId { get; set; }
    public string? Content { get; set; }
    public int? Rating { get; set; }
}
