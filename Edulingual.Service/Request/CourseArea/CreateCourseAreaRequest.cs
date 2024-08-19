using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseArea;

public class CreateCourseAreaRequest
{
    [Required]
    public string Name { get; set; }
}
