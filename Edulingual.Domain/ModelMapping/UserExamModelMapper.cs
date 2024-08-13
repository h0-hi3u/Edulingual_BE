using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class UserExamModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserExam>(entity =>
        {
            entity.ToTable(nameof(UserExam));

            entity.Property(e => e.Id);
            entity.Property(e => e.Score);
            entity.Property(e => e.TotalQuestionRight);

            entity.HasOne(ue => ue.User).WithMany(u => u.UserExams)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ue => ue.Exam).WithMany(e => e.UserExams)
                .HasForeignKey(ue => ue.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
