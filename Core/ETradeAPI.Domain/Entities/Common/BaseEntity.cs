﻿namespace ETradeAPI.Domain.Entities.Common;

public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    //public DateTime DeletedDate { get; set; }
}