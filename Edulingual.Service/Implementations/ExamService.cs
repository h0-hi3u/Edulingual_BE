using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Http;
using Edulingual.Service.Exceptions;
using Edulingual.Caching.Interfaces;
using Edulingual.Domain.Entities;

namespace Edulingual.Service.Implementations;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepo;
    private readonly IQuestionRepository _questionRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;

    public ExamService(IExamRepository examRepo, IQuestionRepository questionRepo, IAnswerRepository answerRepo, IUnitOfWork unitOfWork, IMapper mapper, IDataCached dataCached)
    {
        _examRepo = examRepo;
        _questionRepo = questionRepo;
        _answerRepo = answerRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dataCached = dataCached;
    }

    public Task<ServiceActionResult> CreateExam(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceActionResult> DeleteExam(string id)
    {
        if (!Guid.TryParse(id, out Guid examId)) throw new InvalidParameterException();

        var exam = await _examRepo.GetOneAsync(predicate: e => e.Id == examId) ?? throw new NotFoundException();

        exam.IsDeleted = true;
        _examRepo.Update(exam);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) return new ServiceActionResult($"Delete exam fail: {exam.Title}!");
        await _dataCached.RemoveDataCache<Exam>(id: id);

        return new ServiceActionResult($"Delete exam success: {exam.Title}");
    }

    public Task<ServiceActionResult> GetExam(string id)
    {
        throw new NotImplementedException();
    }
}
