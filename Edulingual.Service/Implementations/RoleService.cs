using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Role;
using Edulingual.Service.Response.Role;
using System.Data.Entity.Migrations.Infrastructure;

namespace Edulingual.Service.Implementations;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepo;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepo, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _roleRepo = roleRepo;
        _mapper = mapper;
    }

    public async Task<AppActionResult> GetAllPaing(int pageIndex = 10, int pageSize = 1)
    {
        if (pageIndex < 1 || pageSize < 1) throw new InvalidParameterException();
        var pagingRole = await _roleRepo.GetPagingAsync(
            predicate: r => !r.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize);
        return new AppActionResult(true) { Data = pagingRole.Mapper<ViewRoleReponse, Role>(_mapper) };
    }

    public async Task<AppActionResult> GetRoleById(string id)
    {
        if (!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();

        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == roleId && !r.IsDeleted);

        if (role == null) throw new NotFoundException();
        return new AppActionResult(true) { Data = _mapper.Map<ViewRoleReponse>(role) };
    }

    public async Task<AppActionResult> CreateRole(CreateRoleRequest createRoleRequest)
    {
        var role = _mapper.Map<Role>(createRoleRequest);
        await _roleRepo.AddAsync(role);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        return new AppActionResult(true, $"Create success! Role: {createRoleRequest.Name}");
    }

    public async Task<AppActionResult> DeleteRole(string id)
    {
        if(!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();

        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id.Equals(roleId) && !r.IsDeleted);
        if (role == null) throw new NotFoundException();

        role.IsDeleted = true;
        _roleRepo.Update(role);
        await _unitOfWork.SaveChangesAsync();
        return new AppActionResult(true, $"Delete success! Role: {role.Name}");
    }

    public async Task<AppActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest, string id)
    {
        if (!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();
        if(roleId != updateRoleRequest.Id) throw new InvalidParameterException();

        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == roleId && !r.IsDeleted);
        if (role == null) throw new NotFoundException();

        role.Name = updateRoleRequest.Name;
        _roleRepo.Update(role);
        await _unitOfWork.SaveChangesAsync();
        return new AppActionResult(true, $"Update success! Role {role.Name}");
    }
}
