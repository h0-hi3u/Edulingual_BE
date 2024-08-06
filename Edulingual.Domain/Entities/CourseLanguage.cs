using Edulingual.Domain.Enum;
using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities
{
    public class CourseLanguage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
    }
}
