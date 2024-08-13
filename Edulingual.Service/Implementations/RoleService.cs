using AutoMapper;
using Edulingual.DAL.Interfaces;
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
        var pagingRole = await _roleRepo.GetPagingAsync(pageIndex: pageIndex, pageSize: pageSize);
        return new AppActionResult(true) { Data = _mapper.Map<IEnumerable<ViewRoleReponse>>(pagingRole) };
    }

    public async Task<AppActionResult> GetRoleById(string id)
    {
        try
        {
            if (!Guid.TryParse(id, out var roleId)) throw new ArgumentException();
            var role = await _roleRepo.GetFirstAsync(predicate: r => r.Id == roleId && !r.IsDeleted);
            return new AppActionResult(true) { Data = _mapper.Map<ViewRoleReponse>(role) };
        }
        catch (Exception ex) 
        {
            throw new Exception();
        }
    }

    public Task<AppActionResult> CreateRole(CreateRoleRequest createRoleRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AppActionResult> DeleteRole(string id)
    {
        throw new NotImplementedException();
    }

    public Task<AppActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest)
    {
        throw new NotImplementedException();
    }


}
