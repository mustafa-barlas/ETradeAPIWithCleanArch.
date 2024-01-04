using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
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
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
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
            var product = await _productReadRepository.GetByIdAsync(id, true);
            return Ok(product);
        }

        //[HttpGet(Name = "get")]
        //public async Task Get()
        //{
        //    await _productWriteRepository.AddRangeAsync(new()
        //        {
        //            new()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Iphone 11 pro max",
        //                Price = 100,
        //                Stock = 10,
        //            },
        //            new()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Iphone 12",
        //                Price = 200,
        //                Stock = 20,
        //            },
        //            new()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Iphone 13 pro",
        //                Price = 300,
        //                Stock = 30,
        //            },
        //        });
        //    await _productWriteRepository.SaveAsync();
        //}
    }
}
