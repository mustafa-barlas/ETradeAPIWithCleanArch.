using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.ProductImageFileRepository;

public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(ETradeApiDbContext context) : base(context)
    {
    }
}