using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;

namespace Edulingual.Service.Implementations;

public class PaymentService : IPaymentService
{
    public Task<ServiceActionResult> CreatePaymentVNPay(Guid userId, int amount, Guid courseId)
    {
           
        return Task.FromResult(new ServiceActionResult("Success"));
    }

    public Task<ServiceActionResult> GetMyPayments(int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceActionResult> GetPayment(string id)
    {
        throw new NotImplementedException();
    }
}
