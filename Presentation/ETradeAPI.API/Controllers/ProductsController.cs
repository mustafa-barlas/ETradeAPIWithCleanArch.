using System.Net;
using ETradeAPI.Application.Repositories.FileRepository;
using ETradeAPI.Application.Repositories.InvoiceFileRepository;
using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Application.RequestParameters;
using ETradeAPI.Application.Services;
using ETradeAPI.Application.ViewModels.Products;
using ETradeAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] Pagination pagination)
        {
            var totalProductCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false)
                .Skip(pagination.Page.Value * pagination.Size.Value).Take(pagination.Size.Value)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Price,
                    x.Stock,
                    x.CreatedDate,
                    x.UpdatedDate
                }).ToList();

            return Ok(new
            {
                products,
                totalProductCount
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveByIdAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var dats = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            //_productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path
            //}).ToList());

            await _productImageFileWriteRepository.SaveAsync();


            //var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            //_invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = new Random().Next()
            //}).ToList());

            //var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            //_fileWriteRepository.AddRangeAsync(datas.Select(d => new ETradeAPI.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //}).ToList());

            //await _fileWriteRepository.SaveAsync();

            //var datas = _fileReadRepository.GetAll();
            //var datas1 = _productImageFileReadRepository.GetAll();
            //var datas2 = _invoiceFileReadRepository.GetAll();
            return Ok();
        }

    }
}
