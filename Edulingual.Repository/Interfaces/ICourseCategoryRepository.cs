using Edulingual.Common.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Interfaces;

public interface ICourseCategoryRepository : IRepository<CourseCategory>, IAutoRegisterable
{
}
