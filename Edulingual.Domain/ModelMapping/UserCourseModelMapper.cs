using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class UserCourseModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.ToTable(nameof(UserCourse));
            entity.Property(e => e.CreatedAt).HasColumnName("CreateAt");
            entity.Property(e => e.CreatedBy).HasColumnName("CreateBy");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdateAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdateBy");

            entity.HasKey(e => new { e.UserId, e.CourseId });
        });
    }
}
