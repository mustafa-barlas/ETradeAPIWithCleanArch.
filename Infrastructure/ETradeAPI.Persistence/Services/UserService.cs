using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Application.Helpers;
using ETradeAPI.Application.Repositories.EndpointRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEndpointReadRepository _endpointReadRepository;

    public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
    {
        _userManager = userManager;
        _endpointReadRepository = endpointReadRepository;
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

    public async Task<List<ListUserDto>> GetAllUsersAsync(int page, int size)
    {
        var users = await _userManager.Users
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        return users.Select(user => new ListUserDto()
        {
            Id = user.Id,
            Email = user.Email,
            NameSurname = user.NameSurname,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName

        }).ToList();
    }

    public int TotalUsersCount => _userManager.Users.Count();

    public async Task AssignRoleToUserAsync(string userId, string[] roles)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {

        AppUser user = await _userManager.FindByIdAsync(userIdOrName);

        if (user == null) user = await _userManager.FindByNameAsync(userIdOrName);


        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        return new string[] { };
    }

    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        var userRoles = await GetRolesToUserAsync(name);

        if (!userRoles.Any()) return false;


        Endpoint? endpoint = await _endpointReadRepository.Table
              .Include(x => x.Roles)
             .FirstOrDefaultAsync(x => x.Code.Equals(code));

        if (endpoint == null) return false;



        var endpointRoles = endpoint.Roles.Select(x => x.Name).ToList();



        foreach (var userRole in userRoles)

            foreach (var endpointRole in endpointRoles)

                if (userRoles.Equals(endpointRoles))
                    return true;



        return false;

        //var hasRole = false;
        //foreach (var userRole in userRoles)
        //{
        //    if (!hasRole)
        //    {
        //        foreach (var endpointRole in endpointRoles)

        //            if (userRoles.Equals(endpointRoles))
        //            {
        //                hasRole = true;
        //                break;
        //            }
        //    }

        //    else break;

        //}

        //return hasRole;

    }
}