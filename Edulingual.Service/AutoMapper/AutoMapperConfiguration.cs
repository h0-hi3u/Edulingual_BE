using AutoMapper;
using Edulingual.Domain.Entities;
using Edulingual.Service.Request.CourseArea;
using Edulingual.Service.Request.CourseCategory;
using Edulingual.Service.Request.CourseLanguage;
using Edulingual.Service.Request.Role;
using Edulingual.Service.Request.User;
using Edulingual.Service.Response.CourseArea;
using Edulingual.Service.Response.CourseCategory;
using Edulingual.Service.Response.CourseLanguage;
using Edulingual.Service.Response.Role;
using Edulingual.Service.Response.User;

namespace Edulingual.Service.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void RegisterMaps(IMapperConfigurationExpression mapper)
    {
        CreateRoleMap(mapper);
        CreatUserMap(mapper);
        CreateRoleMap(mapper);
        CreateCourseLanguageMap(mapper);
        CreateCourseCategoryMap(mapper);
    }

    private static void CreateRoleMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateRoleRequest, Role>();
        mapper.CreateMap<UpdateRoleRequest, Role>();
        mapper.CreateMap<Role, ViewRoleReponse>();
    }
    private static void CreatUserMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateUserRequest, User>();
        mapper.CreateMap<UpdateRoleRequest, User>();
        mapper.CreateMap<User, ViewUserResponse>();
    }
    private static void CreatCourseAreaMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateCourseAreaRequest, CourseArea>();
        mapper.CreateMap<UpdateCourseAreaRequest, CourseArea>();
        mapper.CreateMap<CourseArea, ViewCourseAreaResponse>();
    }
    private static void CreateCourseLanguageMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateCourseLanguageRequest, CourseLanguage>();
        mapper.CreateMap<UpdateCourseLanguageRequest, CourseLanguage>();
        mapper.CreateMap<CourseLanguage, ViewCourseLanguageReponse>();
    }
    private static void CreateCourseCategoryMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateCourseCategoryRequest, CourseCategory>();
        mapper.CreateMap<UpdateCourseCategoryRequest, CourseCategory>();
        mapper.CreateMap<CourseCategory, ViewCourseCategoryResponse>();
    }
}
