using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Constants;
using ETradeAPI.Application.CustomAttributes;
using ETradeAPI.Application.Enums;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;
using ETradeAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ETradeAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETradeAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETradeAPI.Application.Features.Queries.Product.GetAllProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public ProductsController(IMediator mediator, IProductService productService)
        {
            _mediator = mediator;
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }



        [HttpDelete("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product File")]
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product Image")]
        public async Task<IActionResult> DeleteProductImage([FromQuery, FromRoute] RemoveProductImageCommandRequest request)
        {
            RemoveProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change  Showcase Product Image")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest request)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("qrcode/{productId}")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "QRCode To Product")]
        public async Task<IActionResult> GetQrCodeToProduct([FromRoute] string productId)
        {
            var data = await _productService.QrCodeToProductAsync(productId);
            return File(data, "image/png");
        }



        [HttpPut("qrcode")]
        public async Task<IActionResult> UpdateStockQrCodeToProduct(UpdateStockQrCodeToProductCommandRequest request)
        {
            UpdateStockQrCodeToProductCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}