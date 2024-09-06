using AutoMapper;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Feedback;
using Edulingual.Service.Response.Feedback;
using System.Net;

namespace Edulingual.Service.Implementations;

public class FeedbackService : IFeedbachSerivce
{
    private readonly IFeedbackRepository _feedbackRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public FeedbackService(IFeedbackRepository feedbackRepo, ICourseRepository courseRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser)
    {
        _feedbackRepo = feedbackRepo;
        _courseRepo = courseRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<ServiceActionResult> CreateFeedback(CreateFeedbackRequest createFeedbackRequest)
    {
        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == createFeedbackRequest.CourseId) ?? throw new NotFoundException("Not found course!");

        var existing = await _feedbackRepo.GetOneAsync(predicate: f => f.CourseId == createFeedbackRequest.CourseId && f.UserId == _currentUser.CurrentUserId()) ?? throw new InvalidParameterException("You have done feedback for this course!");

        var feedback = _mapper.Map<Feedback>(createFeedbackRequest);

        await _feedbackRepo.AddAsync(feedback);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();

        return new ServiceActionResult("Feedback created!", httpStatusCode: HttpStatusCode.Created);

    }

    public async Task<ServiceActionResult> DeleteFeedback(string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId) ?? throw new NotFoundException("Not found course!");

        var feedback = await _feedbackRepo.GetOneAsync(predicate: f => f.CourseId == courseId && f.UserId == _currentUser.CurrentUserId()) ?? throw new NotFoundException("Not found feedback");

        feedback.IsDeleted = true;
        _feedbackRepo.Update(feedback);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();

        return new ServiceActionResult("Deleted!");
    }

    public async Task<ServiceActionResult> GetFeedbackOfCourse(string id, int pageIndex, int pageSize)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId) ?? throw new NotFoundException("Not found course!");

        var list = await _feedbackRepo.GetPagingAsync(
            predicate: f => f.CourseId == courseId,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        var result = list.Mapper<ViewFeedbackResponse, Feedback>(_mapper);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetMyFeedbackInCourse(string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(c => c.Id == courseId) ?? throw new NotFoundException("Not found course!");

        var feedback = await _feedbackRepo.GetOneAsync(predicate: f => f.CourseId == courseId && f.UserId == _currentUser.CurrentUserId()) ?? throw new NotFoundException("Not found feedbacl");

        var result = _mapper.Map<ViewFeedbackResponse>(feedback);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> UpdateFeedback(UpdateFeedbackRequest updateFeedbackRequest)
    {
        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == updateFeedbackRequest.CourseId) ?? throw new NotFoundException("Not found course!");

        var feedback = await _feedbackRepo.GetOneAsync(predicate: f => f.CourseId == updateFeedbackRequest.CourseId && f.UserId == _currentUser.CurrentUserId()) ?? throw new NotFoundException("Not found feedback!");

        feedback.Content = updateFeedbackRequest.Content ?? feedback.Content;
        feedback.Rating = updateFeedbackRequest.Rating ?? feedback.Rating;
        _feedbackRepo.Update(feedback);
        var isSuccess = await _unitOfWork.SaveChangesAsync();

        return new ServiceActionResult("Update success!");
    }
}
