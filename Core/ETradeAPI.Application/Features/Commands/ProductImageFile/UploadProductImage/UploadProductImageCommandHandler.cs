using ETradeAPI.Application.Abstractions.Storage;
using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using P = ETradeAPI.Domain.Entities;


namespace ETradeAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IStorageService _storageService;

    public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainer)> result = await _storageService.UploadAsync("files", request.Files);

        P.Product product = await _productReadRepository.GetByIdAsync(request.Id);


        await _productImageFileWriteRepository.AddRangeAsync(result.Select(x => new P.ProductImageFile()
        {
            FileName = x.fileName,
            Path = x.pathOrContainer,
            Storage = _storageService.StorageName,
            Products = new List<P.Product>() { product }
        }).ToList());
        await _productImageFileWriteRepository.SaveAsync();

        return new();
    }
}