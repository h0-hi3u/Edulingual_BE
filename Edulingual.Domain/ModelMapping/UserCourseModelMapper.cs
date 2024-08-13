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
        });
    }
}
