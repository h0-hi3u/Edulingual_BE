﻿using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class VNPaysController : BaseApiController
{
    private readonly IVNPayService _vNPayService;

    public VNPaysController(IVNPayService vNPayService)
    {
        _vNPayService = vNPayService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateUrlPayment(string courseId)
    {
        return await ExecuteServiceFunc(
            async () => await _vNPayService.CreatePaymentUrl(courseId).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
