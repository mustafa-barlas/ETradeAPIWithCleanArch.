using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.RemoveByIdAsync(request.Id);
        await _productWriteRepository.SaveAsync();

        return new();
    }
}