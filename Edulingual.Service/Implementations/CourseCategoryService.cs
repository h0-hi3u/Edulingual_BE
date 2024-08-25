using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseCategory;
using Edulingual.Service.Response.CourseArea;
using Edulingual.Service.Response.CourseCategory;
using System.Net;

namespace Edulingual.Service.Implementations;

public class CourseCategoryService : ICourseCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseCategoryRepository _courseCategoryRepo;
    private readonly IMapper _mapper;

    public CourseCategoryService(IUnitOfWork unitOfWork, ICourseCategoryRepository courseCategoryRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _courseCategoryRepo = courseCategoryRepository;
        _mapper = mapper;
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
        var courseCategory = await _courseCategoryRepo.GetOneAsync(predicate: ca => ca.Id == coureCategoryId && !ca.IsDeleted);
        if (courseCategory == null) throw new NotFoundException();

        courseCategory.IsDeleted = true;
        _courseCategoryRepo.Update(courseCategory);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");
        return new ServiceActionResult($"Detele success: {id}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseCategoryRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseCategoryResponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        var list = await _courseCategoryRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        return new ServiceActionResult(list.Mapper<ViewCourseCategoryResponse, CourseCategory>(_mapper));
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseCategoryId)) throw new InvalidParameterException();
        var courseCategory = await _courseCategoryRepo.GetOneAsync(predicate: ca => ca.Id == courseCategoryId && !ca.IsDeleted);
        if (courseCategory == null) throw new NotFoundException();
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
        return new ServiceActionResult($"Update success: {updateCourseCategoryRequest.Name}");
    }
}
