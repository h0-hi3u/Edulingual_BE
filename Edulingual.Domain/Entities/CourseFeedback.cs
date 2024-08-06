using EduLingual.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edulingual.Domain.Entities;

public class CourseFeedback : BaseEntity<Guid>
{
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public Guid FeedbackId { get; set; }
    public virtual Feedback Feedback { get; set; } = null!;
}
