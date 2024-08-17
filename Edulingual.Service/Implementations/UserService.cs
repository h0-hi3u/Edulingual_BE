using AutoMapper;
using System.Net;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Domain.Enum;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.User;
using Microsoft.OpenApi.Extensions;
using Edulingual.Common.Interfaces;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Edulingual.Service.Response.User;
using Edulingual.Service.Extensions;

namespace Edulingual.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public UserService(IUserRepository userRepo, IRoleRepository roleRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<ServiceActionResult> ChangeStatusUser(string id, UserStatusEnum status)
    {
        if (!Guid.TryParse(id, out Guid userId)) throw new InvalidParameterException();

        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == userId && !u.IsDeleted);
        if (user == null) throw new NotFoundException();

        user.Status = status;
        _userRepo.Update(user);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();

        return new ServiceActionResult($"{status.GetDisplayName()} user success!");
    }

    public async Task<ServiceActionResult> CreateUser(CreateUserRequest createUserRequest)
    {
        var user = _mapper.Map<User>(createUserRequest);
        var role = await _roleRepo.GetOneAsync(predicate: r => r.Name == Enum.GetName(RoleEnum.User) && !r.IsDeleted);
        if (role == null) throw new InvalidParameterException("Invalid role!");
        user.RoleId = role.Id;
        user.Status = UserStatusEnum.Active;
        createUserRequest.Password = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password);
        await _userRepo.AddAsync(user);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        return new ServiceActionResult("Create user success!", HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> GetSelfProfile()
    {   
        if (_currentUser.CurrentUserId == null) throw new InvalidParameterException();
        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == _currentUser.CurrentUserId());
        if (user == null) throw new NotFoundException();

        return new ServiceActionResult(_mapper.Map<ViewUserResponse>(user));
    }

    public async Task<ServiceActionResult> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out Guid userId)) throw new InvalidParameterException();
        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == _currentUser.CurrentUserId());
        if (user == null) throw new NotFoundException();

        return new ServiceActionResult(_mapper.Map<ViewUserResponse>(user));
    }

    public async Task<ServiceActionResult> GetUserPagingWithRole(int pageIndex, int pageSize, RoleEnum roleValue)
    {
        var role = await _roleRepo.GetOneAsync(predicate: r => r.Name == Enum.GetName(roleValue) && !r.IsDeleted);
        if (role == null) throw new InvalidParameterException("Invalid role!");

        var list = await _userRepo.GetPagingAsync(
            predicate: u => u.RoleId == role.Id && !u.IsDeleted,
            pageIndex: pageIndex,
            pageSize: pageSize
            );

        return new ServiceActionResult(list.Mapper<ViewUserResponse, User>(_mapper));
    }

    public async Task<ServiceActionResult> UpdateUser(UpdateUserRequest createUserRequest)
    {
        var userId = _currentUser.CurrentUserId();
        if (userId == null) throw new InvalidParameterException();

        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == userId);
        user.Phone = createUserRequest.Phone ?? user.Phone;
        user.FullName = createUserRequest.FullName ?? user.FullName;
        user.Description = createUserRequest.Description ?? user.Description;
        user.ImageUrl = createUserRequest.ImageUrl ?? user.ImageUrl;

        _userRepo.Update(user);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();

        return new ServiceActionResult("Update user success!", HttpStatusCode.NoContent);
    }
}
