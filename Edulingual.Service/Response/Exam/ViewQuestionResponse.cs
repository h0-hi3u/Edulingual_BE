namespace Edulingual.Service.Response.Exam;

public class ViewQuestionResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public double Point { get; set; } = 0;
    public Guid ExamId { get; set; }
    public ICollection<ViewAnswerResponse> Answers { get; set; } = [];
}
