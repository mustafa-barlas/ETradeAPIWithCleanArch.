using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Application;

public static class ServiceRegistrations
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(ServiceRegistrations));
    }
}