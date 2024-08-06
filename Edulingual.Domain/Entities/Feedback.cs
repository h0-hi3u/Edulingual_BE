using EduLingual.Common.Models;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edulingual.Domain.Entities
{
    [Table("feedback")]
    public class Feedback : BaseEntity<Guid>
    {
        public string Description { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; } = FeedbackStatus.Active;
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; } = new List<CourseFeedback>();
    }
}
