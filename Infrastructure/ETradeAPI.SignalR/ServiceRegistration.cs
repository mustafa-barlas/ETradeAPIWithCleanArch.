using ETradeAPI.Application.Abstractions.Hubs;
using ETradeAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.SignalR;

public static class ServiceRegistration
{
    public static void AddSignalRServices(this IServiceCollection collection)
    {
        collection.AddTransient<IProductHubService, ProductHubService>();
        collection.AddTransient<IOrderHubService, OrderHubService>();
        collection.AddSignalR();
    }
}