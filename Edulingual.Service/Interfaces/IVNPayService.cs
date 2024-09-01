using Edulingual.Service.Models;

namespace Edulingual.Service.Interfaces;

public interface IVNPayService
{
    string CreatePaymentUrl(int amount);
    Task<ServiceActionResult> CreatePaymentVNPay(Guid userId, int amount, Guid courseId);
}
