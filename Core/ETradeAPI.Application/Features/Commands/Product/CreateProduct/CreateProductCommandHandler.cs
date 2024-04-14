using ETradeAPI.Application.Abstractions.Hubs;
using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETradeAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductHubService _productHubService;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<CreateProductCommandHandler> logger, IProductHubService productHubService)
    {
        _productWriteRepository = productWriteRepository;
        _logger = logger;
        _productHubService = productHubService;
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
        await _productHubService.ProductAddedMessageAsync($"{request.Name} ürünü eklendi");
        return new();
    }
}