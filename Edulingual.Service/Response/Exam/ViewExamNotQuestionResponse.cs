namespace Edulingual.Service.Response.Exam;

public class ViewExamNotQuestionResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public int TotalQuestion { get; set; } = 0;
}
