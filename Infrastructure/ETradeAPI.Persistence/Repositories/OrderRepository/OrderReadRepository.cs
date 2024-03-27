using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.OrderRepository;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}