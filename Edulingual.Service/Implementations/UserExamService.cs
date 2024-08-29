using Edulingual.DAL.Interfaces;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Exam;
using Microsoft.EntityFrameworkCore;
using Edulingual.Service.Exceptions;
using Edulingual.Common.Interfaces;
using Edulingual.Domain.Enum;
using Edulingual.Domain.Entities;
using System.Net;
using AutoMapper;
using Edulingual.Service.Response.Exam;
using Edulingual.Service.Extensions;
using Edulingual.Service.Response.User;

namespace Edulingual.Service.Implementations;

public class UserExamService : IUserExamService
{
    private readonly IUserExamRepository _userExamRepo;
    private readonly IExamRepository _examRepo;
    private readonly ICurrentUser _currentUser;
    private readonly IUserCourseRepository _userCourseRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepo;
    private readonly IMapper _mapper;

    public UserExamService(IUserExamRepository userExamRepo, IExamRepository examRepo, ICurrentUser currentUser, IUserCourseRepository userCourseRepo, IAnswerRepository answerRepo, IUnitOfWork unitOfWork, ICourseRepository courseRepo, IMapper mapper)
    {
        _userExamRepo = userExamRepo;
        _examRepo = examRepo;
        _currentUser = currentUser;
        _userCourseRepo = userCourseRepo;
        _answerRepo = answerRepo;
        _unitOfWork = unitOfWork;
        _courseRepo = courseRepo;
        _mapper = mapper;
    }

    public async Task<ServiceActionResult> CreateUserExam(CreateExamResultRequest createExamResultRequest)
    {
        var exam = await _examRepo.GetOneAsync(
            predicate: e => e.Id == createExamResultRequest.ExamId && !e.IsDeleted,
            include: e => e.Include(e => e.Course).Include(e => e.Questions)
            ) ?? throw new NotFoundException("Not found course!");

        var userCourse = await _userCourseRepo.GetOneAsync(
            predicate: uc => uc.UserId == _currentUser.CurrentUserId() && uc.CourseId == exam.CourseId && uc.Status == UserCourseStatusEnum.Studying
            ) ?? throw new InvalidParameterException($"User do not exists in course {exam.Course.Title}!");

        double totalRightQuestion = 0;
        
        foreach(var a in createExamResultRequest.AnswerId)
        {
            var answer = await _answerRepo.GetOneAsync(predicate: aw => aw.Id == a);
            if(answer != null && answer.IsCorrect)
            {
                totalRightQuestion += 1;
            }
        }
        var score = totalRightQuestion / exam.TotalQuestion * 10;
        var userExam = new UserExam
        {
            Score = score,
            TotalQuestionRight = totalRightQuestion,
            UserId = (Guid)_currentUser.CurrentUserId()!,
            ExamId = exam.Id,
        };
        await _userExamRepo.AddAsync(userExam);
        var isSucess = await _unitOfWork.SaveChangesAsync();

        if (!isSucess) throw new DatabaseException();
        return new ServiceActionResult("Create user exam sucess!", httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> GetAllExamInCourse(string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();
        var course = await _courseRepo.GetOneAsync(
            predicate: c => c.Id == courseId && !c.IsDeleted,
            include: c => c.Include(c => c.Exams)
            ) ?? throw new NotFoundException("Not found course!");


        var userCourse = await _userCourseRepo.GetOneAsync(
            predicate: uc => uc.UserId == _currentUser.CurrentUserId() && uc.CourseId == course.Id && uc.Status == UserCourseStatusEnum.Studying
            );

        bool isJoinedCourse = userCourse != null;

        if (!isJoinedCourse && course.CreatedBy != _currentUser.CurrentUserId()) throw new InvalidParameterException($"You don't joined to see exam in  course: {course.Title}");

        var result = _mapper.Map<IEnumerable<ViewExamNotQuestionResponse>>(course.Exams);
        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetMyExamDoneInCourse(string id, int pageIndex, int pageSize)
    {
        if (!Guid.TryParse(id, out Guid examId)) throw new InvalidParameterException();

        var exam = await _examRepo.GetOneAsync(predicate: e => e.Id == examId) ?? throw new NotFoundException("Not found exam!");

        var list = await _userExamRepo.GetPagingAsync(
            predicate: ue => ue.ExamId == examId && ue.UserId == _currentUser.CurrentUserId(),
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        var result = list.Mapper<ViewUserResponse, UserExam>(_mapper);

        return new ServiceActionResult(result);
    }
}
