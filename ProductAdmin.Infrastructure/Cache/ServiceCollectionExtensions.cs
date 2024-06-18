namespace ProductAdmin.Infrastructure.Cache;

using Microsoft.Extensions.DependencyInjection;
using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Infrastructure.Cache.Memory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheRepository, MemoryCacheService>();
        return services;
    }
}