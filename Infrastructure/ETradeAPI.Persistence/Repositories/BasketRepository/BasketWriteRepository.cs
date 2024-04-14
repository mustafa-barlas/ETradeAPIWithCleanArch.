using ETradeAPI.Application.Repositories.BasketRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.BasketRepository;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}