using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs;
using MediatR;


namespace ETradeAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{

    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 300);

        return new LoginUserSuccessCommandResponse()
        {
            Token = token
        };
    }
}