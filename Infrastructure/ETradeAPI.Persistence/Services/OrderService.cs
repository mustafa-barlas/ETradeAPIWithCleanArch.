using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.DTOs.Order;
using ETradeAPI.Application.Repositories.CompletedOrderRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    private readonly ICompletedOrderReadRepository _completedOrderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _completedOrderReadRepository = completedOrderReadRepository;
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


        var data2 = from order in data
                    join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                    from _co in co.DefaultIfEmpty()
                    select new
                    {
                        CreatedDate = order.CreatedDate,
                        Id = order.Id,
                        Basket = order.Basket,
                        OrderCode = order.OrderCode,
                        Completed = _co != null ? true : false,
                    };

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Order = await data2.Select(x => new
            {
                CreatedDate = x.CreatedDate,
                OrderCode = x.OrderCode,
                TotalPrice = x.Basket.BasketItems.Sum(x => x.Product.Price * x.Quantity),
                userName = x.Basket.User.UserName,
                x.Completed

            }).ToListAsync()
        };
    }

    public async Task<SingleOrderDto> GetOrderByIdAsync(string id)
    {
        var data = _orderReadRepository.Table
            .Include(x => x.Basket)
            .ThenInclude(x => x.BasketItems)
            .ThenInclude(x => x.Product);



        var data2 = await (from order in data
                           join completedOrder in _completedOrderReadRepository.Table
                               on order.Id equals completedOrder.OrderId into co
                           from _co in co.DefaultIfEmpty()
                           select new
                           {
                               CreatedDate = order.CreatedDate,
                               Id = order.Id,
                               Basket = order.Basket,
                               OrderCode = order.OrderCode,
                               Completed = _co != null ? true : false,
                               Address = order.Address,
                               Description = order.Description,
                           }).FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)));

        return new SingleOrderDto()
        {
            Id = data2.Id.ToString(),
            BasketItems = data2.Basket.BasketItems.Select(x => new
            {
                x.Product.Name,
                x.Product.Price,
                x.Quantity
            }),
            Address = data2.Address,
            OrderCode = data2.OrderCode,
            CreatedDate = data2.CreatedDate,
            Description = data2.Description,
            Completed = data2.Completed,
        };
    }

    public async Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id)
    {
        Order? order = await _orderReadRepository.Table.Include(x => x.Basket).ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(id)));

        if (order is not null)
        {
            await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
            return (await _completedOrderWriteRepository.SaveAsync() > 0, new()
            {
                OrderCode = order.OrderCode,
                Name = order.Basket.User.UserName,
                UserSurname = order.Basket.User.NameSurname,
                OrderDate = order.CreatedDate,
                Email = order.Basket.User.Email
            });
        }

        return (false, null);
    }
}