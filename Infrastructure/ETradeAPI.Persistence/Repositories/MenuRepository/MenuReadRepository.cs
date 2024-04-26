using ETradeAPI.Application.Repositories.MenuRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.MenuRepository;

public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
{
    public MenuReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}