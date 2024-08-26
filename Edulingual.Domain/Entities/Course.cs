using Edulingual.Common.Models;
using Edulingual.Domain.Enum;

namespace Edulingual.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public double Fee { get; set; }
    public Guid OwnerId { get; set; }
    public Guid CourseAreaId { get; set; }
    public Guid CourseLanguageId { get; set; }
    public Guid CourseCategoryId { get; set; }
    public CourseStatusEnum Status { get; set; } = CourseStatusEnum.Pending;


    public virtual User Owner { get; set; }
    public virtual CourseArea CourseArea { get; set; } = null!;
    public virtual CourseCategory CourseCategory { get; set; } = null!;
    public virtual CourseLanguage CourseLanguage { get; set; } = null!;
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
