using System.ComponentModel.DataAnnotations.Schema;
using ETradeAPI.Domain.Entities.Common;

namespace ETradeAPI.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }

    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}