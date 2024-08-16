using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class FeedbackModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable(nameof(Feedback));

            entity.HasKey(e => new { e.UserId, e.CourseId });
            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Rating);
        });
    }
}
