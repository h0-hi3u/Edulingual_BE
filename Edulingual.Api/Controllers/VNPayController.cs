using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VNPayController : BaseApiController
{
    private readonly IVNPayService _vNPayService;

    public VNPayController(IVNPayService vNPayService)
    {
        _vNPayService = vNPayService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateUrlPayment(int amount, string courseId) 
    {
        return await ExecuteServiceFunc(
            async() => await _vNPayService.CreatePaymentUrl(amount, courseId).ConfigureAwait(false)
            ).ConfigureAwait(false);
    } 
}
