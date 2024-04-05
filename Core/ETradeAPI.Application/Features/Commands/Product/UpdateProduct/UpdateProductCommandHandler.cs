using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using P = ETradeAPI.Domain.Entities;
namespace ETradeAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ILogger<UpdateProductCommandHandler> logger)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        P.Product product = await _productReadRepository.GetByIdAsync(request.Id);

        product.Stock = request.Stock;
        product.Price = request.Price;
        product.Name = request.Name;
        product.UpdatedDate = DateTime.Now;

        await _productWriteRepository.SaveAsync();
        _logger.LogInformation("Product Güncellendi...");
        return new();
    }
}