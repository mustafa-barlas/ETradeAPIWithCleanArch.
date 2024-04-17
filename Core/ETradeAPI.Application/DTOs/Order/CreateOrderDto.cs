namespace ETradeAPI.Application.DTOs.Order;

public class CreateOrderDto
{
    public string Description { get; set; }

    public string Address { get; set; }

    public string? BasketId { get; set; }
}