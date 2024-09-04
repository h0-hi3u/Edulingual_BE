using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.CourseArea;
using Edulingual.Service.Response.CourseArea;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Edulingual.Service.Implementations;

public class CourseAreaService : ICourseAreaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAreaRepository _courseAreaRepo;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;

    public CourseAreaService(IUnitOfWork unitOfWork, ICourseAreaRepository courseAreaRepo, IMapper mapper, IDataCached dataCached)
    {
        _unitOfWork = unitOfWork;
        _courseAreaRepo = courseAreaRepo;
        _mapper = mapper;
        _dataCached = dataCached;
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

        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == coureAreaId && !ca.IsDeleted) ?? throw new NotFoundException();

        courseArea.IsDeleted = true;
        _courseAreaRepo.Update(courseArea);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Delete fail: {id}");

        await _dataCached.RemoveDataCache<CourseArea>(id: id);
        return new ServiceActionResult($"Detele success: {courseArea.Name}!");
    }

    public async Task<ServiceActionResult> GetAll()
    {
        var list = await _courseAreaRepo.GetListAsync(predicate: ca => !ca.IsDeleted);
        return new ServiceActionResult(_mapper.Map<IEnumerable<ViewCourseAreaResponse>>(list));
    }

    public async Task<ServiceActionResult> GetAllPaging(int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1) throw new InvalidParameterException();
        var data = await _dataCached.GetDataCache<CourseArea>(pageIndex: pageIndex, pageSize: pageSize);
        if (data != null) 
            return new ServiceActionResult(data);
        
        var list = await _courseAreaRepo.GetPagingAsync(
            predicate: ca => !ca.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );

        var result = list.Mapper<ViewCourseAreaResponse, CourseArea>(_mapper);

        await _dataCached.SetToCache(value: result, pageIndex: pageIndex, pageSize: pageSize);
        return new ServiceActionResult(list.Mapper<ViewCourseAreaResponse, CourseArea>(_mapper));
    }

    public async Task<ServiceActionResult> GetById(string id)
    {
        if (!Guid.TryParse(id, out Guid courseAreaId)) throw new InvalidParameterException();
        var data = _dataCached.GetDataCache<CourseArea>(id: id);
        if (data != null) 
            return new ServiceActionResult(data);

        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == courseAreaId && !ca.IsDeleted) ?? throw new NotFoundException();
        var result = _mapper.Map<ViewCourseAreaResponse>(courseArea);
        await _dataCached.SetToCache(value: result, id: id);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> UpdateCourseArea(UpdateCourseAreaRequest updateCourseAreaRequest)
    {
        var courseArea = await _courseAreaRepo.GetOneAsync(predicate: ca => ca.Id == updateCourseAreaRequest.Id && !ca.IsDeleted) ?? throw new NotFoundException();

        await _dataCached.RemoveDataCache<CourseArea>(id: updateCourseAreaRequest.Id.ToString());

        courseArea.Name = updateCourseAreaRequest.Name;
        _courseAreaRepo.Update(courseArea);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException($"Update fail: {updateCourseAreaRequest.Name}!");

        await _dataCached.RemoveDataCache<CourseArea>(id: updateCourseAreaRequest.Id.ToString());
        return new ServiceActionResult($"Update success: {updateCourseAreaRequest.Name}");
    }
}
