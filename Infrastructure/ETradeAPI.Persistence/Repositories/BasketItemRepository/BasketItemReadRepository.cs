using ETradeAPI.Application.Repositories.BasketItemRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.BasketItemRepository;

public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
{
    public BasketItemReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}