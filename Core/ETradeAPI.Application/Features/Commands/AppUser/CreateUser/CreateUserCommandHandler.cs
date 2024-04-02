using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs.User;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {

        CreateUserResponseDto response = await _userService.CreateAsync(new()
        {
            NameSurname = request.NameSurname,
            Email = request.Email,
            Password = request.Password,
            Username = request.Username,
            PasswordConfirm = request.PasswordConfirm,
        });

        return new()
        {
            Message = response.Message,
            Succeeded = response.Succeeded,
        };
    }
}