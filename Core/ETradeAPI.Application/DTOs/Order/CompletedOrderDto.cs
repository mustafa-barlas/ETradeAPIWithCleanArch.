namespace ETradeAPI.Application.DTOs.Order;

public class CompletedOrderDto
{
    public string OrderCode { get; set; }
    public DateTime OrderDate { get; set; }
    public string Name { get; set; }
    public string UserSurname { get; set; }
    public string Email { get; set; }

}