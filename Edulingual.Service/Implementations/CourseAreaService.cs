using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseArea;
using Edulingual.Service.Response.CourseArea;
using System.Net;

namespace Edulingual.Service.Implementations;

public class CourseAreaService : ICourseAreaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAreaRepository _courseAreaRepo;
    private readonly IMapper _mapper;

    public CourseAreaService(IUnitOfWork unitOfWork, ICourseAreaRepository courseAreaRepo, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _courseAreaRepo = courseAreaRepo;
        _mapper = mapper;
    }

    public async Task<ServiceActionResult> CreateCourseArea(CreateCourseAreaRequest createCourseAreaRequest)
    {
        var courseArea = _mapper.Map<CourseArea>(createCourseAreaRequest);
        await _courseAreaRepo.AddAsync(courseArea);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Create fail: {createCourseAreaRequest.Name}!");
        return new ServiceActionResult($"Create success: {createCourseAreaRequest.Name}!", httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> DeleteCourseArea(string id)
    {
        if (!Guid.TryParse(id, out Guid coureAreaId)) throw new InvalidParameterException();
        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == coureAreaId && !ca.IsDeleted);
        if (courseArea == null) throw new NotFoundException();

        courseArea.IsDeleted = true;
        _courseAreaRepo.Update(courseArea);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");
        return new ServiceActionResult($"Detele success: {courseArea.Name}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseAreaRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseAreaResponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        var list = await _courseAreaRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        return new ServiceActionResult(list.Mapper<ViewCourseAreaResponse, CourseArea>(_mapper));
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseAreaId)) throw new InvalidParameterException();
        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == courseAreaId && !ca.IsDeleted);
        if (courseArea == null) throw new NotFoundException();
        return new ServiceActionResult(_mapper.Map<ViewCourseAreaResponse>(courseArea));
    }

    public async Task<ServiceActionResult> UpdateCourseArea(UpdateCourseAreaRequest updateCourseAreaRequest)
    {
        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == updateCourseAreaRequest.Id && !ca.IsDeleted);
        if (courseArea == null) throw new NotFoundException();

        courseArea.Name = updateCourseAreaRequest.Name;
        _courseAreaRepo.Update(courseArea);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Update fail: {updateCourseAreaRequest.Name}!");
        return new ServiceActionResult($"Update success: {updateCourseAreaRequest.Name}");
    }
}
