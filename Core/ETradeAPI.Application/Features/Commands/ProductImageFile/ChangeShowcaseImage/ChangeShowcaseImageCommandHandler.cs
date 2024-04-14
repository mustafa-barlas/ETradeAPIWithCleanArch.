using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;

public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
    {
        var query = _productImageFileWriteRepository.Table.Include(x => x.Products)
            .SelectMany(x => x.Products, (pif, p) => new
            {
                pif,
                p
            });

        var data = await query.FirstOrDefaultAsync(x => x.p.Id.Equals(Guid.Parse(request.ProductId)) && x.pif.Showcase);

        if (data != null)
        {
            data.pif.Showcase = false;
        }


        var image = await query.FirstOrDefaultAsync(x => x.p.Id.Equals(Guid.Parse(request.ImageId)));
        if (image != null)
        {
            image.pif.Showcase = true;
        }
        
        await _productImageFileWriteRepository.SaveAsync();

        return new();
    }
}