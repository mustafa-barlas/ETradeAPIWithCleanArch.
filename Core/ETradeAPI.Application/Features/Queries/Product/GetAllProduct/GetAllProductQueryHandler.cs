﻿using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false)
        .Skip(request.Page * request.Size).Take(request.Size)
        .Select(x => new
        {
            x.Id,
            x.Name,
            x.Price,
            x.Stock,
            x.CreatedDate,
            x.UpdatedDate
        }).ToList();

        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}