using Edulingual.Service.Models;

namespace Edulingual.Service.Interfaces;

public interface IUserCourseService
{
    Task<ServiceActionResult> StudentJoinCourse(Guid studentId, Guid courseId);
    Task<ServiceActionResult> FinishStudentProgress(Guid studentId, Guid courseId);
}
