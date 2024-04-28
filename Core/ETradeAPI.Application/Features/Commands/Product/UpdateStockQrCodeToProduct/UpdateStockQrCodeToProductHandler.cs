using ETradeAPI.Application.Abstractions.Services;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;

public class UpdateStockQrCodeToProductHandler : IRequestHandler<UpdateStockQrCodeToProductCommandRequest, UpdateStockQrCodeToProductCommandResponse>
{
    private readonly IProductService _productService;

    public UpdateStockQrCodeToProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<UpdateStockQrCodeToProductCommandResponse> Handle(UpdateStockQrCodeToProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);

        return new();
    }
}