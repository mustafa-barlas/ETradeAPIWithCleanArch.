using MediatR;

namespace ETradeAPI.Application.Features.Commands.Role.UpdateRole;

public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    public string Name { get; set; }
    public string Id { get; set; }
}