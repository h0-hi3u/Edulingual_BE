namespace Edulingual.Service.Response.Exam;

public class ViewExamResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public int TotalQuestion { get; set; } = 0;
    public ICollection<ViewQuestionResponse> Questions { get; set; } = [];
}
