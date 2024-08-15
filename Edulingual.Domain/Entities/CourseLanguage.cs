using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class CourseLanguage : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
