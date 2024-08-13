using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class AnswerModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable(nameof(Answer));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Content);
            entity.Property(e => e.IsCorrect);

            entity.HasOne(e => e.Question).WithMany(q => q.Answers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
