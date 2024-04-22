﻿using ETradeAPI.Application.Abstractions.Services;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Basket.AddItemToBasket;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
{
    private readonly IBasketService _basketService;

    public AddItemToBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.AddItemToBasketAsync(new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
        });

        return new();
    }
}