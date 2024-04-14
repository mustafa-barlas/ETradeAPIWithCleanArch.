﻿using ETradeAPI.Application.Abstractions.Services;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
{
    private readonly IBasketService _basketService;

    public GetBasketItemsQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var basketItems = await _basketService.GetBasketItemsAsync();

        return basketItems.Select(x => new GetBasketItemsQueryResponse
        {
            BasketItemId = x.BasketId.ToString(),
            Name = x.Product.Name,
            Price = x.Product.Price,
            Quantity = x.Quantity

        }).ToList();
    }
}