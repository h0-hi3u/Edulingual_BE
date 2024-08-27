using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Implementations;

public class QuestionRepository : Repository<Question>, IQuestionRepository
{
    public QuestionRepository(IApplicationDbContext context, ICurrentUser currentUser) : base(context, currentUser)
    {
    }
}
