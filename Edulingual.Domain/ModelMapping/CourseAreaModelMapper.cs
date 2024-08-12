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
        });
    }
}
