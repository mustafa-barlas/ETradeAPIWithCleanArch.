using ETradeAPI.Application.DTOs.Order;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderDto createOrder);

    Task<ListOrderDto> GetAllOrdersAsync(int page, int size);

    Task<SingleOrderDto> GetOrderByIdAsync(string id);

    Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id);
}