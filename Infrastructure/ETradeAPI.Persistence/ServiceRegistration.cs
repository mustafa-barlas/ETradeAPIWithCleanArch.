using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Application.Repositories.FileRepository;
using ETradeAPI.Application.Repositories.InvoiceFileRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Persistence.Repositories.CustomerRepository;
using ETradeAPI.Persistence.Repositories.OrderRepository;
using ETradeAPI.Persistence.Repositories.ProductRepository;
using ETradeAPI.Persistence.Contexts;
using ETradeAPI.Persistence.Repositories.FileRepository;
using ETradeAPI.Persistence.Repositories.InvoiceFileRepository;
using ETradeAPI.Persistence.Repositories.ProductImageFileRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETradeAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
                 
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
                 
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();


    }
}