using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{

    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        P.Product? product = await _productReadRepository.Table.Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(request.Id)));


        P.ProductImageFile productImage = product.ProductImageFiles.FirstOrDefault(x => x.Id.Equals(Guid.Parse(request.ImageId)));

        product.ProductImageFiles.Remove(productImage);
        await _productWriteRepository.SaveAsync();

        return new();
    }
}