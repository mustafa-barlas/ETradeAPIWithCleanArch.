using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Domain.Entities.Identity;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);

    Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
}