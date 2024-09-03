using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.DAL.Implementations;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(IApplicationDbContext context, ICurrentUser currentUser) : base(context, currentUser)
    {
    }
}
