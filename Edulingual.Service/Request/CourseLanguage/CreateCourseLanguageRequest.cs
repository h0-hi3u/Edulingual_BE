using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.CourseLanguage;

public class CreateCourseLanguageRequest
{
    [Required]
    public string Name { get; set; }
}
