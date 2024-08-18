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
            entity.Property(e => e.CreatedAt).HasColumnName("CreateAt");
            entity.Property(e => e.CreatedBy).HasColumnName("CreateBy");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdateAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdateBy");
        });
    }
}
