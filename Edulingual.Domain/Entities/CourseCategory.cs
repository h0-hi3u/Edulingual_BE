using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EduLingual.Domain.Enum;
using EduLingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class CourseCategory: BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public CourseCategoryStatus Status { get; set; } = CourseCategoryStatus.Available;
    public Guid LanguageId { get; set; }
    public CourseLanguage CourseLanguage { get; set; } = null!;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
