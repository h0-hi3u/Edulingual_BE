using AutoMapper;
using Edulingual.Domain.Entities;
using Edulingual.Service.Request.Role;
using Edulingual.Service.Response.Role;

namespace Edulingual.Service.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void RegisterMaps(IMapperConfigurationExpression mapper)
    {
        CreateRoleMap(mapper);
    }

    public static void CreateRoleMap(IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<CreateRoleRequest, Role>();
        mapper.CreateMap<UpdateRoleRequest, Role>();
        mapper.CreateMap<Role, ViewRoleReponse>();
    }
}
