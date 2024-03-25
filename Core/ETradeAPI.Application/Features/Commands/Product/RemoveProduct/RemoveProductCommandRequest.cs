﻿using MediatR;

namespace ETradeAPI.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
{
    public string Id { get; set; }
}