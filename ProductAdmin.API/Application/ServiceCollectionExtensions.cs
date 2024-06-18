namespace ProductAdmin.API.Application;

using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Application.SystemContext.UseCase;
using ProductAdmin.Application.SystemContext.UseCase.Contracts;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddInventoryUseCases();
        return services;
    }

    public static void AddInventoryUseCases(this IServiceCollection services)
    {
        #region Inventory use cases

        services.AddScoped<IGetCategories, GetCategoriesUseCase>();
        services.AddScoped<IAddProduct, AddProductUseCase>();
        services.AddScoped<IEditProduct, EditProductUseCase>();
        services.AddScoped<IGetProduct, GetProductUseCase>();
        services.AddScoped<IGetProducts, GetProductsUseCase>();

        #endregion Inventory use cases

        #region System use cases

        services.AddScoped<ISetStatusInCache, SetStatusInCacheUseCase>();

        #endregion System use cases
    }

    public static void ConfigurationSystem(this IServiceCollection services, Func<IServiceProvider> serviceProvider)
    {
        services.AddScoped(config =>
        {
            return GetConfigProvider<ISetStatusInCache>(serviceProvider).ExecuteAsync();
        });
    }

    private static T GetConfigProvider<T>(Func<IServiceProvider> serviceProvider)
    {
        using var scope = serviceProvider().CreateScope();
        var provider = scope.ServiceProvider.GetService<T>();

        if (provider == null)
        {
            throw new Exception($"Could not obtain the configuration service: {typeof(T).Name}");
        }

        return provider;
    }
}