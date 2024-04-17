namespace ETradeAPI.Application.Abstractions.Hubs;

public interface IOrderHubService
{
    public Task OrderAddedMessageAsync(string message);
}