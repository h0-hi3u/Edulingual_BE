using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Library;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Edulingual.Service.Implementations;

public class VNPayService : IVNPayService
{
    private readonly IConfiguration _configuration;
    private readonly ICurrentUser _currenUser;
    private readonly VnPayLibrary _vpnPayLibrary;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICourseRepository _courseRepo;

    public VNPayService(IConfiguration configuration, ICurrentUser currenUser, VnPayLibrary vpnPayLibrary, IHttpContextAccessor contextAccessor, ICourseRepository courseRepo)
    {
        _configuration = configuration;
        _currenUser = currenUser;
        _vpnPayLibrary = vpnPayLibrary;
        _contextAccessor = contextAccessor;
        _courseRepo = courseRepo;
    }
    public async Task<ServiceActionResult> CreatePaymentUrl(string courseId)
    {
        if (!Guid.TryParse(courseId, out Guid _courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == _courseId && !c.IsDeleted) ?? throw new NotFoundException();

        var vnpayModel = _configuration.GetSection(nameof(VNPayModel)).Get<VNPayModel>() ??
                           throw new MissingVNPaySettings();
        var vnp_TmnCode = vnpayModel.TmnCode;
        var vnp_HashSecret = vnpayModel.HashSecret;
        var vnp_Url = vnpayModel.Url;
        var vnp_ReturnUrl = vnpayModel.ReturnUrl;
        var vnp_CancelUrl = vnpayModel.CancelUrl;
        var total = course.Fee * 100000;
        var random = new Random();
        var txnRef = random.Next(1, 100000).ToString();
        var clientIp = _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";

        vnp_ReturnUrl = $"{vnp_ReturnUrl}?userId={_currenUser.CurrentUserId()}&amount={course.Fee}&courseId={courseId}";

        _vpnPayLibrary.AddRequestData("vnp_Version", "2.1.0");
        _vpnPayLibrary.AddRequestData("vnp_Command", "pay");
        _vpnPayLibrary.AddRequestData("vnp_TmnCode", vnp_TmnCode);
        _vpnPayLibrary.AddRequestData("vnp_Amount", total.ToString());
        _vpnPayLibrary.AddRequestData("vnp_CurrCode", "VND");
        _vpnPayLibrary.AddRequestData("vnp_TxnRef", txnRef);
        _vpnPayLibrary.AddRequestData("vnp_OrderInfo", "Thanh toan don hang: " + txnRef);
        _vpnPayLibrary.AddRequestData("vnp_OrderType", "Mua dong");
        _vpnPayLibrary.AddRequestData("vnp_Locale", "vn");
        _vpnPayLibrary.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
        _vpnPayLibrary.AddRequestData("vnp_IpAddr", clientIp);
        _vpnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

        string paymentUrl = _vpnPayLibrary.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        return new ServiceActionResult(paymentUrl);
    }
}
