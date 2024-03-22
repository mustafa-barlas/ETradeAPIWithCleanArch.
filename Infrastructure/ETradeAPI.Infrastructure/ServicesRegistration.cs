using ETradeAPI.Application.Abstractions.Storage;
using ETradeAPI.Infrastructure.Enums;
using ETradeAPI.Infrastructure.Services.Storage;
using ETradeAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeAPI.Infrastructure;

public static class ServicesRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();


        //serviceCollection.AddScoped<IFileService, FileService>();
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }

    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.LocalStorage:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            //case StorageType.Azure:
            //    serviceCollection.AddScoped<IStorage, LocalStorage>();
            //    break;
            //case StorageType.AWS:
            //    serviceCollection.AddScoped<IStorage, LocalStorage>();
            //    break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}