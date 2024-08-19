using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseArea;

public interface UpdateCourseAreaRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
}
