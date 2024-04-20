using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Application.Helpers;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETradeAPI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        IdentityResult result = await _userManager.CreateAsync(new AppUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUserDto.Username,
            Email = createUserDto.Email,
            NameSurname = createUserDto.NameSurname,
        }, createUserDto.Password);

        CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

        if (result.Succeeded)
        {
            response.Message = "Kullanıcı kaydı başarılı";
        }
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code}{error.Description}\n";

        return response;

    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
    {

        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        else
        {
            throw new NotFoundUserException();
        }

    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {

        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);

            else
                throw new PasswordChangeFailedException();

        }
    }
}