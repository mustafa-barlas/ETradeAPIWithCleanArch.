using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.ProductImageFileRepository;

public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}