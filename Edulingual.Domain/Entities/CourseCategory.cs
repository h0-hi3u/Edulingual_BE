using Edulingual.Domain.Enum;
using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class CourseCategory: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid LanguageId { get; set; }
    public CourseLanguage CourseLanguage { get; set; } = null!;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
