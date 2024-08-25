using AutoMapper;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Role;
using Edulingual.Service.Response.Role;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using Newtonsoft.Json;
using Edulingual.Caching.Helper;
using Edulingual.Caching.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Edulingual.Common.Models;

namespace Edulingual.Service.Implementations;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepo;
    private readonly IMapper _mapper;
    private readonly IDataCached _dataCached;

    public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepo, IMapper mapper, IDataCached dataCached)
    {
        _unitOfWork = unitOfWork;
        _roleRepo = roleRepo;
        _mapper = mapper;
        _dataCached = dataCached;
    }

    public async Task<ServiceActionResult> GetAllPaing(int pageIndex = 10, int pageSize = 1)
    {
        if (pageIndex < 1 || pageSize < 1) throw new InvalidParameterException();

        var dataFromCached = await _dataCached.GetDataCache<Role>(pageIndex: pageIndex, pageSize: pageSize);
        if (dataFromCached != null) 
        {
            return new ServiceActionResult(dataFromCached);
        }
        var pagingRole = await _roleRepo.GetPagingAsync(
            predicate: r => !r.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize);
        var result = pagingRole.Mapper<ViewRoleReponse, Role>(_mapper);
        if (!result.Data.IsNullOrEmpty())
        {
            await _dataCached.SetToCache(value: result, pageIndex: pageIndex, pageSize: pageSize);
        }
        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetRoleById(string id)
    {
        if (!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();

        var dataFromCached = await _dataCached.GetDataCache<Role>(id);
        if (dataFromCached != null) return new ServiceActionResult(dataFromCached);

        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == roleId && !r.IsDeleted);
        if (role == null) throw new NotFoundException();
        var result = _mapper.Map<ViewRoleReponse>(role);
        await _dataCached.SetToCache(value: result, id: role.Id.ToString());

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> CreateRole(CreateRoleRequest createRoleRequest)
    {
        var role = _mapper.Map<Role>(createRoleRequest);
        await _roleRepo.AddAsync(role);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        return new ServiceActionResult($"Create success! Role: {createRoleRequest.Name}", HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> DeleteRole(string id)
    {
        if(!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();

        if(await _dataCached.GetDataCache<Role>(id) is not null)
        {
            await _dataCached.RemoveDataCache<Role>(id);
        }
        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id.Equals(roleId) && !r.IsDeleted);
        if (role == null) throw new NotFoundException();

        role.IsDeleted = true;
        _roleRepo.Update(role);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        return new ServiceActionResult($"Delete success! Role: {role.Name}");
    }

    public async Task<ServiceActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest, string id)
    {
        if (!Guid.TryParse(id, out Guid roleId)) throw new InvalidParameterException();
        if(roleId != updateRoleRequest.Id) throw new InvalidParameterException();

        if (await _dataCached.GetDataCache<Role>(id) is not null)
        {
            await _dataCached.RemoveDataCache<Role>(id);
        }

        var role = await _roleRepo.GetOneAsync(predicate: r => r.Id == roleId && !r.IsDeleted);
        if (role == null) throw new NotFoundException();

        role.Name = updateRoleRequest.Name;
        _roleRepo.Update(role);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        await _dataCached.SetToCache(value: _mapper.Map<ViewRoleReponse>(role), id: id);
        return new ServiceActionResult($"Update success! Role {role.Name}");
    }
}
