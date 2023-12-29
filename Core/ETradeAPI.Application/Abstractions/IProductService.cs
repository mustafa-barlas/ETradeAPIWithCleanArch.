using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Abstractions;

public interface IProductService
{
    List<Product> GetProducts();
}