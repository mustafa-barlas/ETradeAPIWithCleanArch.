﻿using MediatR;

namespace ETradeAPI.Application.Features.Commands.Role.DeleteRole;

public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
{
    public string Name { get; set; }
}