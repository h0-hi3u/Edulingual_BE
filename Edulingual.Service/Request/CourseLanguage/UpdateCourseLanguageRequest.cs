using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseLanguage;

public class UpdateCourseLanguageRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required] 
    public string Name { get; set; }
}
