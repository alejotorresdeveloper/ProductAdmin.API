namespace ProductAdmin.Infrastructure.Persistency.Files;

using Microsoft.Extensions.DependencyInjection;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Infrastructure.Persistency.Files.Repositories;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFilePersistence(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        return services;
    }
}