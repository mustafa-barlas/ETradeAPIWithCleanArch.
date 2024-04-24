using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Abstractions.Services.Authentications;
using ETradeAPI.Application.Repositories.BasketItemRepository;
using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Application.Repositories.FileRepository;
using ETradeAPI.Application.Repositories.InvoiceFileRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Application.Repositories.ProductImageFileRepository;
using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Domain.Entities.Identity;
using ETradeAPI.Persistence.Repositories.CustomerRepository;
using ETradeAPI.Persistence.Repositories.OrderRepository;
using ETradeAPI.Persistence.Repositories.ProductRepository;
using ETradeAPI.Persistence.Contexts;
using ETradeAPI.Persistence.Repositories.BasketItemRepository;
using ETradeAPI.Persistence.Repositories.FileRepository;
using ETradeAPI.Persistence.Repositories.InvoiceFileRepository;
using ETradeAPI.Persistence.Repositories.ProductImageFileRepository;
using ETradeAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ETradeAPI.Application.Repositories.BasketRepository;
using ETradeAPI.Application.Repositories.CompletedOrderRepository;
using ETradeAPI.Persistence.Repositories.BasketRepository;
using ETradeAPI.Persistence.Repositories.CompletedOrderRepository;
using Microsoft.AspNetCore.Identity;

namespace ETradeAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETradeAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

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

        services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

        services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
        services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;

        }).AddEntityFrameworkStores<ETradeAPIDbContext>()
            .AddDefaultTokenProviders(); // Password Reset token üretmemizi sağlıyor.

    }
}