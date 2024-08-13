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

            entity.HasOne(q => q.Exam).WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
