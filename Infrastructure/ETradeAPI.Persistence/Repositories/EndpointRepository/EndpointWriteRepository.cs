using ETradeAPI.Application.Repositories.EndpointRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.EndpointRepository;

public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}