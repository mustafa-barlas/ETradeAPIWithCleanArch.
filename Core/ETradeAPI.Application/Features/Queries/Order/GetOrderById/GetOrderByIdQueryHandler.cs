using ETradeAPI.Application.Abstractions.Services;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _orderService.GetOrderByIdAsync(request.Id);

        return new()
        {
            Id = data.Id,
            OrderCode = data.OrderCode,
            CreatedDate = data.CreatedDate,
            BasketItems = data.BasketItems,
            Address = data.Address,
            Description = data.Description,
            Completed = data.Completed,
        };
    }
}