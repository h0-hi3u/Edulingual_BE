using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class QuestionModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable(nameof(Question));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Content);
            entity.Property(e => e.Point);
            entity.Property(e => e.CreatedAt).HasColumnName("CreateAt");
            entity.Property(e => e.CreatedBy).HasColumnName("CreateBy");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdateAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdateBy");

            entity.HasOne(q => q.Exam).WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
