using Edulingual.Service.Interfaces;
using Edulingual.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    

    [HttpGet("create-payment")]
    public async Task<IActionResult> CreatePayment([FromQuery] Guid userId, [FromQuery] int amount, [FromQuery] int vnp_ResponseCode, [FromQuery] Guid courseId)
    {
        return await ExecuteServiceFunc(
            async() => await _paymentService.CreatePaymentVNPay(userId, amount, vnp_ResponseCode, courseId).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
