using Edulingual.Domain.Entities;
using Edulingual.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace Edulingual.Domain.ModelMapping;

public class CourseModelMapper : IModelMapper
{
    public void Mapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable(nameof(Course));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Title);
            entity.Property(e => e.Description);
            entity.Property(e => e.Duration).HasMaxLength(200);
            entity.Property(e => e.Fee);
            entity.Property(e => e.Status);

            entity.HasOne(c => c.Owner).WithMany(u => u.OwnedCourse)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(c => c.CourseArea).WithMany(ca => ca.Courses)
                .HasForeignKey(c => c.CourseAreaId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c => c.CourseLanguage).WithMany(cl => cl.Courses)
                .HasForeignKey(c => c.CourseLanguageId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c => c.CourseCategory).WithMany(cc => cc.Courses)
                .HasForeignKey(c => c.CourseCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
