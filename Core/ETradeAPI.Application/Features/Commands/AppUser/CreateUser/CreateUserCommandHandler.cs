using System.Xml;
using ETradeAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using P = ETradeAPI.Domain.Entities.Identity;

namespace ETradeAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<P.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<P.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {


        IdentityResult result = await _userManager.CreateAsync(new P.AppUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
            NameSurname = request.NameSurname,
        }, request.Password);

        CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

        if (result.Succeeded)
        {
            response.Message = "Kullanıcı kaydı başarılı";
        }
        else
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code}{error.Description}\n";
            }

        return response;
        //throw new UserCreateFailedException();

    }
}