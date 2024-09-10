using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseLanguage;
using Edulingual.Service.Response.CourseLanguage;
using System.Net;

namespace Edulingual.Service.Implementations;

public class CourseLanguageService : ICourseLanguageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseLanguageRepository _courseLanguageRepo;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;

    public CourseLanguageService(IUnitOfWork unitOfWork, ICourseLanguageRepository courseLanguageRepo, IMapper mapper, IDataCached dataCached)
    {
        _unitOfWork = unitOfWork;
        _courseLanguageRepo = courseLanguageRepo;
        _mapper = mapper;
        _dataCached = dataCached;
    }

    public async Task<ServiceActionResult> CreateCourseLanguage(CreateCourseLanguageRequest createCourseLanguageRequest)
    {
        var courseLanguage = _mapper.Map<CourseLanguage>(createCourseLanguageRequest);
        await _courseLanguageRepo.AddAsync(courseLanguage);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Create fail: {createCourseLanguageRequest.Name}!");
        return new ServiceActionResult($"Create success: {createCourseLanguageRequest.Name}!", httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> DeleteCourseLanguage(string id)
    {
        if (!Guid.TryParse(id, out Guid coureLanguageId)) throw new InvalidParameterException();
        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == coureLanguageId && !ca.IsDeleted);
        if (courseLanguage == null) throw new NotFoundException();

        courseLanguage.IsDeleted = true;
        _courseLanguageRepo.Update(courseLanguage);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");

        await _dataCached.RemoveDataCache<CourseLanguage>(id: id);
        return new ServiceActionResult($"Detele success: {id}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseLanguageRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseLanguageReponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1) throw new InvalidParameterException();
        var totalRecord = await _courseLanguageRepo.CountAsync();
        int totalPage = totalRecord != 0 ? (int)Math.Ceiling(totalRecord / (double)pageSize) : 0;

        if (totalPage < pageIndex) throw new InvalidParameterException($"Page index need smaller than {totalPage}");
        var data = _dataCached.GetDataCache<CourseCategory>(pageIndex: pageIndex, pageSize: pageSize);
        if (data != null) return new ServiceActionResult(data);

        var list = await _courseLanguageRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        var result = list.Mapper<ViewCourseLanguageReponse, CourseLanguage>(_mapper);
        await _dataCached.SetToCache(value: result, pageIndex: pageIndex, pageSize: pageSize);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseLanguageId)) throw new InvalidParameterException();

        var data = await _dataCached.GetDataCache<CourseLanguage>(id: id);
        if (data != null) return new ServiceActionResult(data);

        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == courseLanguageId && !ca.IsDeleted) ?? throw new NotFoundException();
        var result = _mapper.Map<ViewCourseLanguageReponse>(courseLanguage);
        await _dataCached.SetToCache(value: result, id: id);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> UpdateCourseLanguage(UpdateCourseLanguageRequest updateCourseLanguageRequest)
    {
        var courseLanguage = await _courseLanguageRepo.GetOneAsync(predicate: ca => ca.Id == updateCourseLanguageRequest.Id && !ca.IsDeleted);
        if (courseLanguage == null) throw new NotFoundException();

        courseLanguage.Name = updateCourseLanguageRequest.Name;
        _courseLanguageRepo.Update(courseLanguage);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Update fail: {updateCourseLanguageRequest.Name}!");
        await _dataCached.RemoveDataCache<CourseLanguage>(id: updateCourseLanguageRequest.Id.ToString());
        return new ServiceActionResult($"Update success: {updateCourseLanguageRequest.Name}");
    }
}
