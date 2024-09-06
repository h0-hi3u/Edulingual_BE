using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseCategory;
using Edulingual.Service.Response.CourseCategory;
using System.Net;

namespace Edulingual.Service.Implementations;

public class CourseCategoryService : ICourseCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseCategoryRepository _courseCategoryRepo;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;

    public CourseCategoryService(IUnitOfWork unitOfWork, ICourseCategoryRepository courseCategoryRepository, IMapper mapper, IDataCached dataCached)
    {
        _unitOfWork = unitOfWork;
        _courseCategoryRepo = courseCategoryRepository;
        _mapper = mapper;
        _dataCached = dataCached;
    }

    public async Task<ServiceActionResult> CreateCourseCategory(CreateCourseCategoryRequest createCourseCategoryRequest)
    {
        var courseCategory = _mapper.Map<CourseCategory>(createCourseCategoryRequest);
        await _courseCategoryRepo.AddAsync(courseCategory);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Create fail: {createCourseCategoryRequest.Name}!");
        return new ServiceActionResult($"Create success: {createCourseCategoryRequest.Name}!", httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> DeleteCourseCategory(string id)
    {
        if (!Guid.TryParse(id, out Guid coureCategoryId)) throw new InvalidParameterException();
        var courseCategory = await _courseCategoryRepo.GetOneAsync(predicate: ca => ca.Id == coureCategoryId && !ca.IsDeleted) ?? throw new NotFoundException();

        courseCategory.IsDeleted = true;
        _courseCategoryRepo.Update(courseCategory);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");

        await _dataCached.RemoveDataCache<CourseCategory>(id: id);
        return new ServiceActionResult($"Detele success: {id}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseCategoryRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseCategoryResponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        var data = _dataCached.GetDataCache<CourseCategory>(pageIndex: pageIndex, pageSize: pageSize);
        if (data != null) return new ServiceActionResult(data);

        var list = await _courseCategoryRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        var result = list.Mapper<ViewCourseCategoryResponse, CourseCategory>(_mapper);
        await _dataCached.SetToCache(value: result, pageIndex: pageIndex, pageSize: pageSize);
        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseCategoryId)) throw new InvalidParameterException();

        var data = await _dataCached.GetDataCache<CourseCategory>(id: id);
        if (data != null) return new ServiceActionResult(data);

        var courseCategory = await _courseCategoryRepo.GetOneAsync(predicate: ca => ca.Id == courseCategoryId && !ca.IsDeleted) ?? throw new NotFoundException();
        var result = _mapper.Map<ViewCourseCategoryResponse>(courseCategory);
        await _dataCached.SetToCache(value: result, id: id);
        return new ServiceActionResult(_mapper.Map<ViewCourseCategoryResponse>(courseCategory));
    }

    public async Task<ServiceActionResult> UpdateCourseCategory(UpdateCourseCategoryRequest updateCourseCategoryRequest)
    {
        var courseCategory = await _courseCategoryRepo.GetOneAsync(predicate: ca => ca.Id == updateCourseCategoryRequest.Id && !ca.IsDeleted);
        if (courseCategory == null) throw new NotFoundException();

        courseCategory.Name = updateCourseCategoryRequest.Name;
        _courseCategoryRepo.Update(courseCategory);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Update fail: {updateCourseCategoryRequest.Name}!");

        await _dataCached.RemoveDataCache<CourseCategory>(id: updateCourseCategoryRequest.Id.ToString());
        return new ServiceActionResult($"Update success: {updateCourseCategoryRequest.Name}");
    }
}
