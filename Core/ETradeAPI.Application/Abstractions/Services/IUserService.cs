﻿using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Domain.Entities.Identity;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);

    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);

    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);

    Task<List<ListUserDto>> GetAllUsersAsync(int page, int size);

    int TotalUsersCount { get; }

    Task AssignRoleToUserAsync(string userId, string[] roles);

    Task<string[]> GetRolesToUserAsync(string userIdOrName);

    //Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
}