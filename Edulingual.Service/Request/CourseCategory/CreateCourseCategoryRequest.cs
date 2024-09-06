using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseCategory;

public class CreateCourseCategoryRequest
{
    [Required]
    public string Name { get; set; } = null!;
}
