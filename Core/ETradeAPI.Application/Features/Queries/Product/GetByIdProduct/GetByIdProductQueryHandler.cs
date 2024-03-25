using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using P = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    private readonly IProductReadRepository _repository;

    public GetByIdProductQueryHandler(IProductReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        P.Product product = await _repository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
        };
    }
}