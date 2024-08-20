using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseCategory;

public class UpdateCourseCategoryRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
