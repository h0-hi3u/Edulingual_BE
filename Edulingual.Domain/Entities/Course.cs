using Edulingual.Common.Models;
using Edulingual.Domain.Enum;

namespace Edulingual.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public double Tuitionfee { get; set; } = 0;
    public CourseStatus Status { get; set; } = CourseStatus.Pending;
    public bool IsHighlighted { get; set; } = false;
    public Guid CourseAreaId { get; set; }
    public virtual CourseArea CourseArea { get; set; } = null!;
    public Guid CourseLanguageId { get; set; }
    public virtual CourseLanguage CourseLanguage { get; set; } = null!;
    public Guid CourseCategoryId { get; set; }
    public virtual CourseCategory CourseCategory { get; set; } = null!;
    public Guid CenterId { get; set; }
    public virtual User Center { get; set; } = null!;
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; } = new List<CourseFeedback>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
