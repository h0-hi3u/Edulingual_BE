using AutoMapper;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Response.Payment;

namespace Edulingual.Service.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepo;
    private readonly IUserCourseRepository _userCourseRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    public PaymentService(IPaymentRepository paymentRepo, IUserCourseRepository courseRepo, IMapper mapper, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _paymentRepo = paymentRepo;
        _userCourseRepo = courseRepo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ServiceActionResult> CreatePaymentVNPay(Guid userId, int amount, int vnp_ResponseCode, Guid courseId)
    {
        if (vnp_ResponseCode == 00)
        {
            var existingPayment = await _paymentRepo.GetOneAsync(predicate: p => p.UserId == userId && p.CourseId == courseId);
            if (existingPayment != null) throw new InvalidParameterException("You have payment for this course!");
            var payment = new Payment
            {
                Fee = amount,
                UserId = userId,
                CourseId = courseId
            };
            await _paymentRepo.AddAsync(payment);

            var existingUserCourse = await _userCourseRepo.GetOneAsync(predicate: uc => uc.UserId == userId && uc.CourseId == courseId);
            if (existingUserCourse != null) throw new InvalidParameterException("You joined course!");
            var userCourse = new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
                CreatedBy = userId,
                UpdatedBy = userId,
            };
            await _userCourseRepo.AddAsync(userCourse);

            var isSuccess = await _unitOfWork.SaveChangesAsync();
            if (!isSuccess) throw new DatabaseException();
            return new ServiceActionResult("Create payment success!");
        }
        return new ServiceActionResult("Fail in banking!");
    }

    public async Task<ServiceActionResult> GetMyPayments(int pageIndex, int pageSize)
    {
        var list = await _paymentRepo.GetPagingAsync(
            predicate: p => p.UserId == _currentUser.CurrentUserId(),
            pageIndex: pageIndex,
            pageSize: pageSize
            );

        var result = list.Mapper<ViewPaymentResponse, Payment>(_mapper);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetPayment(string id)
    {
        if (!Guid.TryParse(id, out Guid paymentId)) throw new InvalidParameterException();

        var payment = await _paymentRepo.GetOneAsync(predicate: p => p.Id == paymentId) ?? throw new NotFoundException();

        var result = _mapper.Map<ViewPaymentResponse>(payment);

        return new ServiceActionResult(result);
    }
}
