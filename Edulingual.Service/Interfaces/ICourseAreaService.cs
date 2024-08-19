using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseArea;

namespace Edulingual.Service.Interfaces;

public interface ICourseAreaService : IAutoRegisterable
{
    Task<ServiceActionResult> GetAll();
    Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize);
    Task<ServiceActionResult> GetById(string id);
    Task<ServiceActionResult> CreateCourseArea(CreateCourseAreaRequest createCourseAreaRequest);
    Task<ServiceActionResult> UpdateCourseArea(UpdateCourseAreaRequest updateCourseAreaRequest);
    Task<ServiceActionResult> DeleteCourseArea(string id);
}
