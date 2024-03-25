using ETradeAPI.Domain.Entities;
using P = ETradeAPI.Domain.Entities;
namespace ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryResponse
{
    public string Name { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

}