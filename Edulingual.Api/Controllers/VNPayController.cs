using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Edulingual.Api.Controllers;

public class VNPayController : BaseApiController
{
    private readonly IVNPayService _vNPayService;

    public VNPayController(IVNPayService vNPayService)
    {
        _vNPayService = vNPayService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateUrlPayment(string courseId) 
    {
        return await ExecuteServiceFunc(
            async() => await _vNPayService.CreatePaymentUrl(courseId).ConfigureAwait(false)
            ).ConfigureAwait(false);
    } 
}
