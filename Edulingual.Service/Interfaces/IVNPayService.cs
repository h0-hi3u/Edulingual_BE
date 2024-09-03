using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;

namespace Edulingual.Service.Interfaces;

public interface IVNPayService : IAutoRegisterable
{
    Task<ServiceActionResult> CreatePaymentUrl(string courseId);
}
