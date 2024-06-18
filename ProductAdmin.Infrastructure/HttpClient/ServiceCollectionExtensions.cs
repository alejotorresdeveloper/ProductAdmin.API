namespace ProductAdmin.Infrastructure.HttpClients;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Infrastructure.HttpClients.DiscountAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        string discountApiUrlKey = "ExternalsAPIs:DiscountAPI:BaseUrl";

        string urlBase = configuration.GetSection(discountApiUrlKey).Value
            ?? throw new ArgumentNullException(discountApiUrlKey);

        services.AddHttpClient("DiscountAPI").ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(urlBase);
        });

        services.AddScoped<IDiscountService, DiscountService>();

        return services;
    }
}