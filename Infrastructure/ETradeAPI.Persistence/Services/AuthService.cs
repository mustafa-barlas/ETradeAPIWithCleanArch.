using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Abstractions.Token;
using ETradeAPI.Application.DTOs;
using ETradeAPI.Application.DTOs.Facebook;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ETradeAPI.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserService _userService;

    public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, HttpClient httpClient, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClient;
        _configuration = configuration;
        _signInManager = signInManager;
        _userService = userService;
    }



    async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
    {
        bool result = user != null;

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result) // AspNetUserLogins
        {
            await _userManager.AddLoginAsync(user, info);
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 500);

            return token;
        }

        throw new Exception("Invalid external authentication ");
    }


    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync
            ($"https://praph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponseDto? facebookAccessTokenResponseDto = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug-token?input_token={authToken}&access_token={facebookAccessTokenResponseDto?.AccessToken}");


        FacebookUserAccessTokenValidationDto? validation =
            JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

        if (validation?.Data.IsValid is null)
        {
            string userInfoResponse = await _httpClient.GetStringAsync($"https://praph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponseDto? userInfo =
                JsonSerializer.Deserialize<FacebookUserInfoResponseDto>(userInfoResponse);



            var info = new UserLoginInfo("FACEBOOK", validation?.Data.UserId, "FACEBOOK");

            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        }
        throw new Exception("Invalid external authentication ");

    }


    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
    }


    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);

        if (user is null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user is null)
            throw new NotFoundUserException();


        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }

        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken.Equals(refreshToken));

        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(300, user);
            _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }
        else
        {
            throw new NotFoundUserException();
        }
    }
}