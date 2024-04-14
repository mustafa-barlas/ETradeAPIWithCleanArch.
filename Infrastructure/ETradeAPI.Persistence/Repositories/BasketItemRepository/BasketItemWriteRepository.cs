using ETradeAPI.Application.Repositories.BasketItemRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.BasketItemRepository;

public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
{
    public BasketItemWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}