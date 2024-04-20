using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs.Order;
using ETradeAPI.Application.Repositories.OrderRepository;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
    }


    public async Task CreateOrderAsync(CreateOrderDto createOrder)
    {
        var orderCode = (new Random().NextDouble() * 10000).ToString();
        orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

        await _orderWriteRepository.AddAsync(new()
        {
            Address = createOrder.Address,
            Id = Guid.Parse(createOrder.BasketId),
            Description = createOrder.Description,
            OrderCode = orderCode,

        });
        await _orderWriteRepository.SaveAsync();
    }

    public async Task<ListOrderDto> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderReadRepository.Table
              .Include(x => x.Basket)
              .ThenInclude(x => x.User)
              .Include(x => x.Basket)
              .ThenInclude(x => x.BasketItems)
              .ThenInclude(x => x.Product);

        var data = query.Skip(page * size).Take(size);


        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Order = await data.Select(x => new
            {
                CreatedDate = x.CreatedDate,
                OrderCode = x.OrderCode,
                TotalPrice = x.Basket.BasketItems.Sum(x => x.Product.Price * x.Quantity),
                userName = x.Basket.User.UserName

            }).ToListAsync()
        };
    }

    public async Task<SingleOrderDto> GetOrderById(string id)
    {
        var data = await _orderReadRepository.Table
            .Include(x => x.Basket)
            .ThenInclude(x => x.BasketItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)));

        return new()
        {
            Id = data.Id.ToString(),
            BasketItems = data.Basket.BasketItems.Select(x => new
            {
                x.Product.Name,
                x.Product.Price,
                x.Quantity
            }),
            Address = data.Address,
            OrderCode = data.OrderCode,
            CreatedDate = data.CreatedDate,
            Description = data.Description
        };
    }
}