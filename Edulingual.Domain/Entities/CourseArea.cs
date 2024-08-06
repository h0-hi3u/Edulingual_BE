using Edulingual.Domain.Enum;
using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class CourseArea : BaseEntity
{ 
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
