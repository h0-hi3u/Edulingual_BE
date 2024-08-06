using EduLingual.Common.Models;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edulingual.Domain.Entities
{
    [Table("user")]
    public class User : BaseEntity<Guid>
    {
        [Column("username")]
        [Required]
        public string UserName { get; set; } = null!;

        [Column("password")]
        [Required]
        public string Password { get; set; } = null!;

        [Column("fullname")]
        [Required]
        public string FullName { get; set; } = null!;

        [Column("email")]
        [Required]
        public string Email { get; set; } = null!;

        [Column("image_url")]
        public string ImageUrl { get; set; } = null!;

        [Column("description")]
        [StringLength(1000)]
        public string Description { get; set; } = null!;


        [Column("status")]
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; } = UserStatus.Active;

        [Column("role_id")]
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        [InverseProperty(nameof(User))]
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

        [InverseProperty("Center")]
        public virtual ICollection<Course> OwnCourses { get; set; } = new List<Course>();

        [InverseProperty(nameof(User))]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        [InverseProperty("Center")]
        public virtual ICollection<Exam> OwnExams { get; set; } = new List<Exam>();

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
    }
}
