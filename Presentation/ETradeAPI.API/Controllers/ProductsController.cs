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


        [HttpGet(Name = "getAll")]
        public IActionResult GetProducts()
        {

            var result = _productReadRepository.GetAll();
            return Ok(result);
        }

        //[HttpGet(Name = "get")]
        //public async void Get()
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
