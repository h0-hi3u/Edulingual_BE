using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Payment : BaseEntity
{
    public double Fee { get; set; } = 0;
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }

    public Course Course { get; set; } = null!;
    public User User { get; set; } = null!;
}
