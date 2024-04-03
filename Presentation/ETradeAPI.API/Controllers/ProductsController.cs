using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETradeAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETradeAPI.Application.Features.Queries.Product.GetAllProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }



        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery, FromForm] UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromQuery, FromRoute] RemoveProductImageCommandRequest request)
        {
            RemoveProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}