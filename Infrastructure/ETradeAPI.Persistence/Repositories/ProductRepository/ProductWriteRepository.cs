using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.ProductRepository;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ETradeApiDbContext context) : base(context)
    {
    }
}