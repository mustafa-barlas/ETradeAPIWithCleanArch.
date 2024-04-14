using ETradeAPI.Domain.Entities.Common;
using ETradeAPI.Domain.Entities.Identity;

namespace ETradeAPI.Domain.Entities;

public class Basket : BaseEntity
{
    public string UserId { get; set; }

    public AppUser User { get; set; }

    public Order Order { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
}