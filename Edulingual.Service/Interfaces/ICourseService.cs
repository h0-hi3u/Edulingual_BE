using Edulingual.Common.Interfaces;
using Edulingual.Domain.Enum;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Course;
using Edulingual.Service.Request.Search;

namespace Edulingual.Service.Interfaces;

public interface ICourseService : IAutoRegisterable
{
    Task<ServiceActionResult> GetCoursePaging(int pageIndex, int pageSize);
    Task<ServiceActionResult> SearchCourse(SearchCourse searchCourse);
    Task<ServiceActionResult> ChangeStatusCourse(string id, CourseStatusEnum status);
    Task<ServiceActionResult> CreateCourse(CreateCourseRequest createCourseRequest);
    Task<ServiceActionResult> UpdateCourse(UpdateCourseRequest updateCourseRequest);
    Task<ServiceActionResult> DeleteCourse(string id);
}
