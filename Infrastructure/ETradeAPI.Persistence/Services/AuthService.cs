﻿using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Abstractions.Token;
using ETradeAPI.Application.DTOs;
using ETradeAPI.Application.DTOs.Facebook;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Application.Helpers;
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
    private readonly IMailService _mailService;

    public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, HttpClient httpClient, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClient;
        _configuration = configuration;
        _signInManager = signInManager;
        _userService = userService;
        _mailService = mailService;
    }



    async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
    {
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
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

        if (result)
        {
            await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        throw new Exception("Invalid external authentication.");
    }
    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponseDto? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidationDto? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponseDto? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDto>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        }
        throw new Exception("Invalid external authentication.");
    }
    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded) //Authentication başarılı!
        {
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(15, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }
        else
            throw new NotFoundUserException();
    }

    public async Task PasswordResetAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
            resetToken = resetToken.UrlEncode();

            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
        }
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //resetToken = Encoding.UTF8.GetString(tokenBytes);
            resetToken = resetToken.UrlDecode();

            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        }
        return false;
    }
}