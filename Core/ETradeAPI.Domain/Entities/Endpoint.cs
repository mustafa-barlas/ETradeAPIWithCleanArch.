using ETradeAPI.Domain.Entities.Common;
using ETradeAPI.Domain.Entities.Identity;

namespace ETradeAPI.Domain.Entities;

public class Endpoint : BaseEntity
{
    public Endpoint()
    {
        Roles = new HashSet<AppRole>();
    }

    public string Code { get; set; }

    public Menu Menu { get; set; }

    public string ActionType { get; set; }

    public string HttpType { get; set; }

    public string Definition { get; set; }

    public ICollection<AppRole> Roles { get; set; }
}