using ETradeAPI.Application.Repositories.ProductRepository;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {

            var result = _productReadRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
          var product = await _productReadRepository.GetByIdAsync(id,false);
          product.Name = "aslı";
          await _productWriteRepository.SaveAsync();
          return Ok(product);
        }

        //[HttpGet]
        //public async Task Get()
        //{
        //    await _productWriteRepository.AddRangeAsync(new()
        //    {
        //        new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "product 1",
        //            Price = 100,
        //            Stock = 10,
        //            CreatedDate = DateTime.UtcNow
        //        },
        //        new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "product 2",
        //            Price = 200,
        //            Stock = 20,
        //            CreatedDate = DateTime.UtcNow
        //        },
        //        new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "product 3",
        //            Price = 300,
        //            Stock = 30,
        //            CreatedDate = DateTime.UtcNow
        //        },
        //    });
        //    await _productWriteRepository.SaveAsync();
        //}
    }
}
