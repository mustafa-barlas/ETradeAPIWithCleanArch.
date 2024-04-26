using ETradeAPI.Application.Repositories.EndpointRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.EndpointRepository;

public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
{
    public EndpointReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}