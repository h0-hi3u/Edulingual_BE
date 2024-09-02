using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;

namespace Edulingual.Service.Interfaces;

public interface IPaymentService : IAutoRegisterable
{
    Task<ServiceActionResult> GetMyPayments(int pageIndex, int pageSize);
    Task<ServiceActionResult> GetPayment(string id);
    Task<ServiceActionResult> CreatePaymentVNPay(Guid userId, int amount, Guid courseId);
}
