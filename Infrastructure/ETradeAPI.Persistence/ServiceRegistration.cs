using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Persistence.Repositories.CustomerRepository;
using ETradeAPI.Persistence.Repositories.OrderRepository;
using ETradeAPI.Persistence.Repositories.ProductRepository;
using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETradeAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),ServiceLifetime.Singleton);

        services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
        services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
                 
        services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
        services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
                 
        services.AddSingleton<IProductReadRepository, ProductReadRepository>();
        services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();


    }
}