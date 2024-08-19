using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseLanguage;
using Edulingual.Service.Response.CourseLanguage;

namespace Edulingual.Service.Implementations;

public class CourseLanguageService : ICourseLanguageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseLanguageRepository _courseLanguageRepo;
    private readonly IMapper _mapper;

    public CourseLanguageService(IUnitOfWork unitOfWork, ICourseLanguageRepository courseLanguageRepo, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _courseLanguageRepo = courseLanguageRepo;
        _mapper = mapper;
    }

    public async Task<ServiceActionResult> CreateCourseLanguage(CreateCourseLanguageRequest createCourseLanguageRequest)
    {
        var courseLanguage = _mapper.Map<CourseLanguage>(createCourseLanguageRequest);
        await _courseLanguageRepo.AddAsync(courseLanguage);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Create fail: {createCourseLanguageRequest.Name}!");
        return new ServiceActionResult($"Create success: {createCourseLanguageRequest.Name}!");
    }

    public async Task<ServiceActionResult> DeleteCourseLanguage(string id)
    {
        if (!Guid.TryParse(id, out Guid coureLanguageId)) throw new InvalidParameterException();
        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == coureLanguageId && !ca.IsDeleted);
        if (courseLanguage == null) throw new NotFoundException();

        courseLanguage.IsDeleted = true;
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");
        return new ServiceActionResult($"Detele success: {id}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseLanguageRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseLanguageReponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        var list = await _courseLanguageRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseLanguageReponse>>(list));
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseLanguageId)) throw new InvalidParameterException();
        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == courseLanguageId && !ca.IsDeleted);
        if (courseLanguage == null) throw new NotFoundException();
        return new ServiceActionResult(courseLanguage);
    }

    public async Task<ServiceActionResult> UpdateCourseLanguage(UpdateCourseLanguageRequest updateCourseLanguageRequest)
    {
        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == updateCourseLanguageRequest.Id && !ca.IsDeleted);
        if (courseLanguage == null) throw new NotFoundException();

        courseLanguage.Name = updateCourseLanguageRequest.Name;
        _courseLanguageRepo.Update(courseLanguage);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Update fail: {updateCourseLanguageRequest.Name}!");
        return new ServiceActionResult($"Update success: {updateCourseLanguageRequest.Name}");
    }
}
