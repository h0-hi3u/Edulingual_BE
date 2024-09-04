using Edulingual.Service.Interfaces;
using Edulingual.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet("vnpay-payment")]
    public async Task<IActionResult> CreatePayment([FromQuery] Guid userId, [FromQuery] int amount, [FromQuery] int vnp_ResponseCode, [FromQuery] Guid courseId)
    {
        return await ExecuteServiceFunc(
            async() => await _paymentService.CreatePaymentVNPay(userId, amount, vnp_ResponseCode, courseId).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("my-payments")]
    public async Task<IActionResult> GetMyPayments([FromQuery] int pageIndex, [FromQuery] int pageSize) 
    {
        return await ExecuteServiceFunc(
            async() => await _paymentService.GetMyPayments(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _paymentService.GetPayment(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
