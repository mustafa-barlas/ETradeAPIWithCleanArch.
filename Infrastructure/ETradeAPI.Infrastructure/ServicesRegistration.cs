using ETradeAPI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Infrastructure;

public static class ServicesRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileService, IFileService>();
    }
}