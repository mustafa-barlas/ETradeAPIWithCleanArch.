using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using P = ETradeAPI.Domain.Entities;
namespace ETradeAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        P.Product product = await _productReadRepository.GetByIdAsync(request.Id);

        product.Stock = request.Stock;
        product.Price = request.Price;
        product.Name = request.Name;
        product.UpdatedDate = DateTime.Now;

        await _productWriteRepository.SaveAsync();

        return new();
    }
}