using ETradeAPI.Application.DTOs.Order;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IOrderService
{
    public Task CreateOrderAsync(CreateOrderDto createOrder);
}