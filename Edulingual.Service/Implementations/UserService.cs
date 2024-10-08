﻿using AutoMapper;
using Edulingual.Caching.Interfaces;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Interfaces;
using Edulingual.Domain.Entities;
using Edulingual.Domain.Enum;
using Edulingual.Service.Exceptions;
using Edulingual.Service.Extensions;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.User;
using Edulingual.Service.Response.User;
using Microsoft.OpenApi.Extensions;
using System.Net;

namespace Edulingual.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IDataCached _dataCached;

    public UserService(IUserRepository userRepo, IRoleRepository roleRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser, IDataCached dataCached)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _dataCached = dataCached;
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
        var existing = await _userRepo.GetOneAsync(u => u.Email == createUserRequest.Email); 
        if (existing != null) throw new InvalidParameterException("Email is existing in system!");

        var user = _mapper.Map<User>(createUserRequest);
        var role = await _roleRepo.GetOneAsync(predicate: r => r.Name == Enum.GetName(RoleEnum.Student) && !r.IsDeleted);
        if (role == null) throw new InvalidParameterException("Invalid role!");

        user.RoleId = role.Id;
        user.Status = UserStatusEnum.Active;
        user.Password = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password);
        await _userRepo.AddAsync(user);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        return new ServiceActionResult("Create user success!", HttpStatusCode.Created);
    }

    public async Task<ServiceActionResult> GetSelfProfile()
    {
        if (_currentUser.CurrentUserId() == null) throw new InvalidParameterException();

        var data = _dataCached.GetDataCache<User>(id: _currentUser.CurrentUserId().ToString()!);
        if (data != null) return new ServiceActionResult(data);

        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == _currentUser.CurrentUserId()) ?? throw new NotFoundException();
        var result = _mapper.Map<ViewUserResponse>(user);
        await _dataCached.SetToCache(value: result, id: _currentUser.CurrentUserId().ToString()!);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out Guid userId)) throw new InvalidParameterException();

        var data = _dataCached.GetDataCache<User>(id: id);
        if (data != null) return new ServiceActionResult(data);

        var user = await _userRepo.GetOneAsync(predicate: u => u.Id == userId && !u.IsDeleted && u.Status != UserStatusEnum.Banned) ?? throw new NotFoundException();
        var result = _mapper.Map<ViewUserResponse>(user);
        await _dataCached.SetToCache(value: result, id: id);

        return new ServiceActionResult(result);
    }

    public async Task<ServiceActionResult> GetUserPagingWithRole(int pageIndex, int pageSize, RoleEnum roleValue)
    {
        var role = await _roleRepo.GetOneAsync(predicate: r => r.Name == Enum.GetName(roleValue) && !r.IsDeleted) ?? throw new InvalidParameterException("Invalid role!");

        if (pageIndex < 1 || pageSize < 1) throw new InvalidParameterException();
        var totalRecord = await _userRepo.CountAsync(u => u.RoleId == role.Id && !u.IsDeleted && u.Status != UserStatusEnum.Banned);
        int totalPage = totalRecord != 0 ? (int)Math.Ceiling(totalRecord / (double)pageSize) : 0;

        if (totalPage < pageIndex) throw new InvalidParameterException($"Page index need smaller than {totalPage}");

        var list = await _userRepo.GetPagingAsync(
            predicate: u => u.RoleId == role.Id && !u.IsDeleted && u.Status != UserStatusEnum.Banned,
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
        if (user == null) throw new NotFoundException();
        user.Phone = createUserRequest.Phone ?? user.Phone;
        user.FullName = createUserRequest.FullName ?? user.FullName;
        user.Description = createUserRequest.Description ?? user.Description;
        user.ImageUrl = createUserRequest.ImageUrl ?? user.ImageUrl;

        _userRepo.Update(user);
        var isSuccess = await _unitOfWork.SaveChangesAsync();
        if (!isSuccess) throw new DatabaseException();
        await _dataCached.RemoveDataCache<User>(id: _currentUser.CurrentUserId().ToString()!);

        return new ServiceActionResult("Update user success!", HttpStatusCode.NoContent);
    }
}
