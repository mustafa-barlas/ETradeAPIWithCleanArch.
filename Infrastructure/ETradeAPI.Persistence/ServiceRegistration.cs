using ETradeAPI.Application.Abstractions;
using ETradeAPI.Persistence.Concrete;
using Microsoft.Extensions.DependencyInjection;
namespace ETradeAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductService, ProductService>();
    }
}