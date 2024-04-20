using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Exceptions;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.AppUser.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }


    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Password.Equals(request.PasswordConfirm))
            throw new PasswordChangeFailedException("Şifreler aynı değil");

        await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

        return new();
    }
}