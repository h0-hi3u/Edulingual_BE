using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EduLingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class CourseArea : BaseEntity<Guid>
{ 
    public string Name { get; set; } = string.Empty;
    public CourseAreaStatus Status { get; set; } = CourseAreaStatus.Available;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
