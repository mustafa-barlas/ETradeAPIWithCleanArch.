using ETradeAPI.Application.Repositories.CompletedOrderRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.CompletedOrderRepository;

public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder> , ICompletedOrderWriteRepository
{
    public CompletedOrderWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}