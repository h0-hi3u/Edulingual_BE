using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Exceptions;

namespace Edulingual.Service.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public PaymentService(IPaymentRepository paymentRepo, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _paymentRepo = paymentRepo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceActionResult> CreatePaymentVNPay(Guid userId, int amount, int vnp_ResponseCode, Guid courseId)
    {
        if (vnp_ResponseCode == 00)
        {
            var existing = await _paymentRepo.GetOneAsync(predicate: p => p.UserId == userId && p.CourseId == courseId);
            if (existing != null) throw new InvalidParameterException("You joined course!");
            var payment = new Payment
            {
                Fee = amount,
                UserId = userId,
                CourseId = courseId
            };
            await _paymentRepo.AddAsync(payment);
            var isSuccess = await _unitOfWork.SaveChangesAsync();
            if (!isSuccess) throw new DatabaseException();
            return new ServiceActionResult("Create payment success!");
        }
        return new ServiceActionResult("Fail in banking!");
    }

    public Task<ServiceActionResult> GetMyPayments(int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceActionResult> GetPayment(string id)
    {
        throw new NotImplementedException();
    }
}
