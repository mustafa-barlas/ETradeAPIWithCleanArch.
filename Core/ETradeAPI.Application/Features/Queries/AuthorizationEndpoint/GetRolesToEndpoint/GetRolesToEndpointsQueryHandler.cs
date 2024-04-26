using ETradeAPI.Application.Abstractions.Services;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;

public class GetRolesToEndpointsQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
{
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public GetRolesToEndpointsQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code, request.Menu);

        return new()
        {
            Roles = datas
        };
    }
}