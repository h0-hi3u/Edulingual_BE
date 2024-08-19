using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Implementations;

public class CourseAreaRepository : Repository<CourseArea>, ICourseAreaRepository
{
    public CourseAreaRepository(IApplicationDbContext context, ICurrentUser currentUser) : base(context, currentUser)
    {
    }
}
