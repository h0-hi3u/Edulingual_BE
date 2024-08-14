using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Role;
using Edulingual.Service.Response.Role;

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

    public async Task<AppActionResult> GetAllPaing(int pageIndex, int pageSize)
    {
        try
        {
            var pagingRole = await _roleRepo.GetPagingAsync(
                predicate: r => !r.IsDeleted,
                pageIndex: pageIndex,
                pageSize: pageSize);
            return new AppActionResult(true) { Data = _mapper.Map<IEnumerable<ViewRoleReponse>>(pagingRole) };
        }
        catch (Exception ex)
        {
            throw new DatabaseException(ex.Message);
        }
    }

    public async Task<AppActionResult> GetRoleById(string id)
    {
        try
        {
            if (!Guid.TryParse(id, out var roleId)) throw new InvalidParameterException();
            var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == roleId && !r.IsDeleted);

            if (role == null) throw new NotFoundException();
            return new AppActionResult(true) { Data = _mapper.Map<ViewRoleReponse>(role) };
        }
        catch (Exception ex) 
        {
            throw new DatabaseException(ex.Message);
        }
    }

    public async Task<AppActionResult> CreateRole(CreateRoleRequest createRoleRequest)
    {
        try
        {
            var role = _mapper.Map<Role>(createRoleRequest);
            await _roleRepo.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return new AppActionResult(true, $"Create success! Role: {createRoleRequest.RoleName}");
        }
        catch (Exception ex)
        {
            throw new DatabaseException(ex.Message);
        }
    }

    public async Task<AppActionResult> DeleteRole(string id)
    {
        try
        {
            if(!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();

            var role = await _roleRepo.GetOneAsync(predicate: r => r.Id.Equals(roleId) && !r.IsDeleted);
            if (role == null) throw new NotFoundException();

            role.IsDeleted = true;
            _roleRepo.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return new AppActionResult(true, $"Delete success! Role: {role.RoleName}");
        }
        catch (Exception ex) 
        {
            throw new DatabaseException(ex.Message);
        }

    }

    public async Task<AppActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest)
    {
        try
        {
            var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == updateRoleRequest.Id && !r.IsDeleted);
            if (role == null) throw new NotFoundException();

            role.RoleName = updateRoleRequest.RoleName;
            _roleRepo.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return new AppActionResult(true, $"Update success! Role {role.RoleName}");
        }
        catch (Exception ex) 
        {
            throw new DatabaseException(ex.Message);
        }
    }
}
