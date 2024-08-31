using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Http;
using Edulingual.Service.Exceptions;
using Edulingual.Caching.Interfaces;
using Edulingual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Edulingual.Service.Response.Exam;
using Edulingual.Common.Interfaces;
using OfficeOpenXml;

namespace Edulingual.Service.Implementations;

public class ExamService : IExamService
{
    private readonly ICourseRepository _courseRepo;
    private readonly IExamRepository _examRepo;
    private readonly IQuestionRepository _questionRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;
    private readonly ICurrentUser _currentUser;

    public ExamService(ICourseRepository courseRepo, IExamRepository examRepo, IQuestionRepository questionRepo, IAnswerRepository answerRepo, IUnitOfWork unitOfWork, IMapper mapper, IDataCached dataCached, ICurrentUser currentUser)
    {
        _courseRepo = courseRepo;
        _examRepo = examRepo;
        _questionRepo = questionRepo;
        _answerRepo = answerRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dataCached = dataCached;
        _currentUser = currentUser;
    }

    public async Task<ServiceActionResult> CreateExamFromExcel(string id, IFormFile file)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId && !c.IsDeleted && c.CreatedBy == _currentUser.CurrentUserId()) ?? throw new NotFoundException();

        if (file == null || file.Length == 0) throw new InvalidParameterException();
        var exam = new Exam();
        exam.CourseId = courseId;

        exam.Title = file.FileName.Split(".")[0];
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                var rowCount = worksheet.Dimension.Rows;
                var colCount = 6;
                // Read the content of the Excel file (Example: read the first cell)
                for (int i = 2; i <= rowCount; i++)
                {
                    var question = new Question();
                    question.ExamId = exam.Id;
                    question.Content = worksheet.Cells[i, 1].Text;
                    for (int j = 2; j < colCount; j++)
                    {
                        var answer = new Answer
                        {
                            Content = worksheet.Cells[i, j].Text,
                            IsCorrect = j == 2,
                            QuestionId = question.Id
                        };
                        question.Answers.Add(answer);
                    }
                    question.Point = double.Parse(worksheet.Cells[i, colCount].Text);
                    exam.Questions.Add(question);
                }
            }
        }

        exam.TotalQuestion = exam.Questions.Count();

        await _examRepo.AddAsync(exam);
        var isSuccessful = await _unitOfWork.SaveChangesAsync();
        if (!isSuccessful) throw new DatabaseException();

        return new ServiceActionResult($"Create success exam: {exam.Title}!");
    }


    public async Task<ServiceActionResult> DeleteExam(string id)
    {
        if (!Guid.TryParse(id, out Guid examId)) throw new InvalidParameterException();

        var exam = await _examRepo.GetOneAsync(predicate: e => e.Id == examId && e.CreatedBy == _currentUser.CurrentUserId()) ?? throw new NotFoundException();

        exam.IsDeleted = true;
        _examRepo.Update(exam);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) return new ServiceActionResult($"Delete exam fail: {exam.Title}!");
        await _dataCached.RemoveDataCache<Exam>(id: id);

        return new ServiceActionResult($"Delete exam success: {exam.Title}");
    }

    public async Task<ServiceActionResult> GetExam(string id)
    {
        if (!Guid.TryParse(id, out Guid examId)) throw new InvalidParameterException();

        var data = await _dataCached.GetDataCache<Exam>(id: id);
        if (data != null) return new ServiceActionResult(data);

        var exam = await _examRepo.GetOneAsync(
            predicate: e => e.Id == examId && !e.IsDeleted,
            include: e => e.Include(e => e.Questions).ThenInclude(q => q.Answers)
            ) ?? throw new NotFoundException();

        var result = _mapper.Map<ViewExamResponse>(exam);
        await _dataCached.SetToCache(value: result, id: id);
        return new ServiceActionResult(result);
    }
}
