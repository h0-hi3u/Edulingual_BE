using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Implementations;

public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(IApplicationDbContext context, ICurrentUser currentUser) : base(context, currentUser)
    {
    }
}
