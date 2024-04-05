using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETradeAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<CreateProductCommandHandler> logger)
    {
        _productWriteRepository = productWriteRepository;
        _logger = logger;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
        });

        await _productWriteRepository.SaveAsync();
        _logger.LogInformation("Product Eklendi");
        return new();
    }
}