using ETradeAPI.Application.Repositories.BasketRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.BasketRepository;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
    public BasketReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}