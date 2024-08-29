using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Exam;

namespace Edulingual.Service.Interfaces;

public interface IUserExamService : IAutoRegisterable
{
    Task<ServiceActionResult> GetMyExamDoneInCourse(string id, int pageIndex, int pageSize);
    Task<ServiceActionResult> GetAllExamInCourse(string id);
    Task<ServiceActionResult> CreateUserExam(CreateExamResultRequest createExamResultRequest);
}
