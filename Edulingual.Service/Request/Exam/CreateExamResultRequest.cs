namespace Edulingual.Service.Request.Exam;

public class CreateExamResultRequest
{
    public Guid ExamId { get; set; }
    public List<Guid> AnswerId { get; set; }
}
