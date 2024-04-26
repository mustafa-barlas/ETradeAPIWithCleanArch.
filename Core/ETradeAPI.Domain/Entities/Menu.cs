using ETradeAPI.Domain.Entities.Common;

namespace ETradeAPI.Domain.Entities;
 
public class Menu : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Endpoint> Endpoints { get; set; }
}