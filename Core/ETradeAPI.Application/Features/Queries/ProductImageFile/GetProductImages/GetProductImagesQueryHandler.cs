using ETradeAPI.Application.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        P.Product? product = await _productReadRepository.Table.Include(x => x.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(request.Id)));


        return product.ProductImageFiles.Select(x => new GetProductImagesQueryResponse()
        {
            FileName = x.FileName,
            Id = x.Id,
            Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
        }).ToList();
    }
}