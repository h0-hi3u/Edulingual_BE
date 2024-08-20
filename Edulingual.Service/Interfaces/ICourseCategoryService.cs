using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseCategory;

namespace Edulingual.Service.Interfaces;

public interface ICourseCategoryService : IAutoRegisterable
{
    Task<ServiceActionResult> GetAll();
    Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize);
    Task<ServiceActionResult> GetById(string id);
    Task<ServiceActionResult> CreateCourseCategory(CreateCourseCategoryRequest createCourseCategoryRequest);
    Task<ServiceActionResult> UpdateCourseCategory(UpdateCourseCategoryRequest updateCourseCategoryRequest);
    Task<ServiceActionResult> DeleteCourseCategory(string id);
}
