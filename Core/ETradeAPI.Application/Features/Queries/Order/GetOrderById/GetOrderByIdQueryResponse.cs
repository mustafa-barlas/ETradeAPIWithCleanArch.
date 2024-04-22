﻿namespace ETradeAPI.Application.Features.Queries.Order.GetOrderById;

public class GetOrderByIdQueryResponse
{
    public string Description { get; set; }

    public string Address { get; set; }

    public string OrderCode { get; set; }

    public Object BasketItems { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Id { get; set; }

    public bool Completed { get; set; }

}