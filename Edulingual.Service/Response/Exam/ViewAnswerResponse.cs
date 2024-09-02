using Edulingual.Domain.Entities;

namespace Edulingual.Service.Response.Exam;

public class ViewAnswerResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
    public Guid QuestionId { get; set; }
}
