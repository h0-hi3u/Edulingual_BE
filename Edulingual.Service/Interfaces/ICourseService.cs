﻿using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Course;
using Edulingual.Service.Request.Search;

namespace Edulingual.Service.Interfaces;

public interface ICourseService : IAutoRegisterable
{
    Task<ServiceActionResult> GetCoursePaging(int pageIndex, int pageSize);
    Task<ServiceActionResult> SearchCourse(SearchCourse searchCourse);
    Task<ServiceActionResult> ChangeStatusCourse(string id);
    Task<ServiceActionResult> CreateCourse(CreateCourseRequest createCourseRequest);
    Task<ServiceActionResult> UpdateCourse(UpdateCourseRequest updateCourseRequest, string id);
    Task<ServiceActionResult> DeleteCourse(string id);
    Task<ServiceActionResult> GetMyCourses(int pageIndex, int pageSize);
}
