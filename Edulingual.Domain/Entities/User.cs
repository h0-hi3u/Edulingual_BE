using Edulingual.Common.Models;
using Edulingual.Domain.Enum;

namespace Edulingual.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public Guid RoleId { get; set; }


        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Exam> OwnExams { get; set; } = new List<Exam>();
        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
    }
}
