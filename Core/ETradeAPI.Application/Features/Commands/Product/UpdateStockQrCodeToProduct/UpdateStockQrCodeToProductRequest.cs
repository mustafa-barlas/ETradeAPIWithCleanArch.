﻿using MediatR;

namespace ETradeAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;

public class UpdateStockQrCodeToProductCommandRequest : IRequest<UpdateStockQrCodeToProductCommandResponse>
{
    public string ProductId { get; set; }
    public int Stock { get; set; }
}