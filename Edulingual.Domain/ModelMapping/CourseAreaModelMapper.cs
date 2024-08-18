using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edulingual.Domain.ModelMapping;

public class CourseAreaModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseArea>(entity =>
        {
            entity.ToTable(nameof(CourseArea));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnName("CreateAt");
            entity.Property(e => e.CreatedBy).HasColumnName("CreateBy");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdateAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdateBy");
        });
    }
}
