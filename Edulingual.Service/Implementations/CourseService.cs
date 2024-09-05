using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Extensions;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Domain.Enum;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Course;
using Edulingual.Service.Request.Search;
using Edulingual.Service.Response.Course;
using LinqKit;
using System.Net;

namespace Edulingual.Service.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepo;
    private readonly ICourseAreaRepository _courseAreaRepo;
    private readonly ICourseLanguageRepository _courseLanguageRepo;
    private readonly ICourseCategoryRepository _courseCategoryRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IDataCached _dataCached;

    public CourseService(ICourseRepository courseRepo, ICourseAreaRepository courseAreaRepo, ICourseLanguageRepository courseLanguageRepo, ICourseCategoryRepository courseCategoryRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser, IDataCached dataCached)
    {
        _courseRepo = courseRepo;
        _courseAreaRepo = courseAreaRepo;
        _courseLanguageRepo = courseLanguageRepo;
        _courseCategoryRepo = courseCategoryRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _dataCached = dataCached;
    }

    public async Task<ServiceActionResult> ChangeStatusCourse(string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId && !c.IsDeleted) ?? throw new NotFoundException();
        course.Status = course.Status == CourseStatusEnum.Pending ? CourseStatusEnum.Active : CourseStatusEnum.Pending;
        _courseRepo.Update(course);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();

        return new ServiceActionResult($"Change status {course.Title} success");
    }

    public async Task<ServiceActionResult> CreateCourse(CreateCourseRequest createCourseRequest)
    {
        if (await _courseAreaRepo.GetOneAsync(ca => ca.Id == createCourseRequest.CourseAreaId) is null)
            throw new InvalidParameterException();
        if (await _courseCategoryRepo.GetOneAsync(cc => cc.Id == createCourseRequest.CourseCategoryId) is null)
            throw new InvalidParameterException();
        if (await _courseLanguageRepo.GetOneAsync(cl => cl.Id == createCourseRequest.CourseLanguageId) is null)
            throw new InvalidParameterException();

        var course = _mapper.Map<Course>(createCourseRequest);
        course.OwnerId = _currentUser.CurrentUserId() ?? Guid.NewGuid();
        await _courseRepo.AddAsync(course);
        var isSucess = await _unitOfWork.SaveChangesAsync();
        if (!isSucess) throw new DatabaseException();

        return new ServiceActionResult($"Create {course.Title} success!", httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> DeleteCourse(string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId && !c.IsDeleted)
            ?? throw new NotFoundException();

        course.IsDeleted = true;
        _courseRepo.Update(course);
        var isSucess = await _unitOfWork.SaveChangesAsync();
        if (!isSucess) throw new DatabaseException();
        await _dataCached.RemoveDataCache<Course>(id: id);

        return new ServiceActionResult($"Delete {course.Title} success!");
    }

    public async Task<ServiceActionResult> GetCoursePaging(int pageIndex, int pageSize)
    {
        var data = await _dataCached.GetDataCache<Course>(pageIndex: pageIndex, pageSize: pageSize);
        if (data != null) return new ServiceActionResult(data);

        var list = await _courseRepo.GetPagingAsync(
            predicate: c => !c.IsDeleted && c.Status == CourseStatusEnum.Active,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        var result = list.Mapper<ViewCourseResponse, Course>(_mapper);
        await _dataCached.SetToCache(value: result, pageIndex: pageIndex, pageSize: pageSize);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> SearchCourse(SearchCourse searchCourse)
    {
        var predicate = PredicateBuilder.New<Course>();

        if (!string.IsNullOrEmpty(searchCourse.LanguageId))
        {
            if (!Guid.TryParse(searchCourse.LanguageId, out Guid languageId)) throw new InvalidParameterException("Invalid course language id!");
            //query = query.Where(c => c.CourseLanguageId == languageId);
            predicate = predicate.And(c => c.CourseLanguageId == languageId);
        }

        if (!string.IsNullOrEmpty(searchCourse.AreaId))
        {
            if (!Guid.TryParse(searchCourse.AreaId, out Guid areaId)) throw new InvalidParameterException("Invalid course area id!");
            //query = query.Where(c => c.CourseAreaId == areaId);
            predicate = predicate.And(c => c.CourseAreaId == areaId);
        }

        if (!string.IsNullOrEmpty(searchCourse.CategoryId))
        {
            if (!Guid.TryParse(searchCourse.CategoryId, out Guid categoryId)) throw new InvalidParameterException("Invalid course category id!");
            //query = query.Where(c => c.CourseCategoryId == categoryId);
            predicate = predicate.And(c => c.CourseCategoryId == categoryId);
        }

        if (searchCourse.PriceFrom < 0 || searchCourse.PriceTo < 0 || (searchCourse.PriceFrom > searchCourse.PriceTo)) throw new InvalidParameterException("Invalid price search!");
        if (searchCourse.PriceFrom > 0 || searchCourse.PriceTo > 0)
        {
            //query = query.Where(c => c.Fee >= searchCourse.PriceFrom && c.Fee <= searchCourse.PriceTo);
            predicate = predicate.And(c => c.Fee >= searchCourse.PriceFrom && c.Fee <= searchCourse.PriceTo);
        }

        IQueryable<Course> query = _courseRepo.GetAll().Where(predicate);
        var list = await query.ToPagingAsync(
            pageIndex: searchCourse.PageIndex,
            pageSize: searchCourse.PageSize);
        return new ServiceActionResult(list.Mapper<ViewCourseResponse, Course>(_mapper));
    }

    public async Task<ServiceActionResult> UpdateCourse(UpdateCourseRequest updateCourseRequest, string id)
    {
        if (!Guid.TryParse(id, out Guid courseId)) throw new InvalidParameterException();

        var course = await _courseRepo.GetOneAsync(predicate: c => c.Id == courseId && c.CreatedBy == _currentUser.CurrentUserId() && !c.IsDeleted);
        if (course == null) throw new NotFoundException();

        course.Title = updateCourseRequest.Title ?? course.Title;
        course.Description = updateCourseRequest.Description ?? course.Title;
        course.Duration = updateCourseRequest.Duration ?? course.Duration;
        course.Fee = updateCourseRequest.Fee ?? course.Fee;
        course.CourseLanguageId = updateCourseRequest.CourseLanguageId ?? course.CourseLanguageId;
        course.CourseAreaId = updateCourseRequest.CourseAreaId ?? course.CourseAreaId;
        course.CourseCategoryId = updateCourseRequest.CourseCategoryId ?? course.CourseCategoryId;

        _courseRepo.Update(course);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        await _dataCached.RemoveDataCache<Course>(id: updateCourseRequest.Id.ToString());

        return new ServiceActionResult($"Update {course.Title} success!");
    }
    public async Task<ServiceActionResult> GetMyCourses(int pageIndex, int pageSize)
    {
        var list = await _courseRepo.GetPagingAsync(
            predicate: c => c.CreatedBy == _currentUser.CurrentUserId() && !c.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );
        return new ServiceActionResult(list.Mapper<ViewCourseResponse, Course>(_mapper));
    }
}
