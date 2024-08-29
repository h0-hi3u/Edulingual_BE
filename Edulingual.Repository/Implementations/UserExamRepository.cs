using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Implementations;

public class UserExamRepository : Repository<UserExam>, IUserExamRepository
{
    public UserExamRepository(IApplicationDbContext context, ICurrentUser currentUser) : base(context, currentUser)
    {
    }
}
