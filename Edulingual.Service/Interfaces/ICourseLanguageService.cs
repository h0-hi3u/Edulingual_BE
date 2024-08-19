using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseLanguage;

namespace Edulingual.Service.Interfaces;

public interface ICourseLanguageService : IAutoRegisterable
{
    Task<ServiceActionResult> GetAll();
    Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize);
    Task<ServiceActionResult> GetById(string id);
    Task<ServiceActionResult> CreateCourseLanguage(CreateCourseLanguageRequest createCourseLanguageRequest);
    Task<ServiceActionResult> UpdateCourseLanguage(UpdateCourseLanguageRequest updateCourseLanguageRequest);
    Task<ServiceActionResult> DeleteCourseLanguage(string id);
}
