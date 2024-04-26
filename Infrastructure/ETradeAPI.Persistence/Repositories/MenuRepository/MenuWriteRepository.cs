using ETradeAPI.Application.Repositories.MenuRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.MenuRepository;

public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}