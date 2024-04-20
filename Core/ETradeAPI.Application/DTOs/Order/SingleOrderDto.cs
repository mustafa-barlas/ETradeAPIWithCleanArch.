namespace ETradeAPI.Application.DTOs.Order;

public class SingleOrderDto
{
    public string Description { get; set; }

    public string Address { get; set; }

    public string OrderCode { get; set; }

    public Object BasketItems { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Id { get; set; }

}