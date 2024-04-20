using ETradeAPI.Application.DTOs.Order;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IOrderService
{
    public Task CreateOrderAsync(CreateOrderDto createOrder);

    public Task<ListOrderDto> GetAllOrdersAsync(int page, int size);

    public Task<SingleOrderDto> GetOrderById(string id);
}