using ETradeAPI.Application.Abstractions.Token;
using ETradeAPI.Application.DTOs;
using ETradeAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using P = ETradeAPI.Domain.Entities.Identity;
// ReSharper disable CommentTypo



namespace ETradeAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<P.AppUser> _userManager;
    private readonly SignInManager<P.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(
        UserManager<P.AppUser> userManager,
        SignInManager<P.AppUser> signInManager,
        ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        P.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();


        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            Token token = _tokenHandler.CreateAccessToken(5);
            return new LoginUserSuccessCommandResponse() { Token = token };
        }

        throw new AuthenticationErrorException();
    }
}