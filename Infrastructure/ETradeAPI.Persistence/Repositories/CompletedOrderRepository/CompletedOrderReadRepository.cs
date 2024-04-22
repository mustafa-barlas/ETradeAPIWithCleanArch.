using ETradeAPI.Application.Repositories.CompletedOrderRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.CompletedOrderRepository;

public class CompletedOrderReadRepository : ReadRepository<CompletedOrder>, ICompletedOrderReadRepository
{
    public CompletedOrderReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}