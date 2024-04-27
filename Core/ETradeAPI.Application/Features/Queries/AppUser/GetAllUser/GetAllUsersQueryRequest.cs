﻿using MediatR;

namespace ETradeAPI.Application.Features.Queries.AppUser.GetAllUser;

public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
{
    public int Page { get; set; } = 0;

    public int Size { get; set; } = 5;
}