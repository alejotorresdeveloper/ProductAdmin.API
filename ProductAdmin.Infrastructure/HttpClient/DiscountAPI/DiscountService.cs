namespace ProductAdmin.Infrastructure.HttpClients.DiscountAPI;

using Microsoft.Extensions.Configuration;
using ProductAdmin.Application.DomainServices.Repositories;
using System.Net.Http;
using System.Text.Json;

public class DiscountService : IDiscountService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly string _discountEndpoint;

    public DiscountService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        _httpClientFactory = httpClientFactory;

        string discountEndPointKey = "ExternalsAPIs:DiscountAPI:Resource";

        _discountEndpoint = configuration.GetSection(discountEndPointKey).Value
            ?? throw new ArgumentNullException(discountEndPointKey);

        _httpClient = _httpClientFactory.CreateClient("DiscountAPI")
            ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async ValueTask<int> GetDiscount(int productId)
    {
        HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, $"{_httpClient.BaseAddress}/{_discountEndpoint}/{productId}");

        HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

        httpResponseMessage.EnsureSuccessStatusCode();

        string response = await httpResponseMessage.Content.ReadAsStringAsync();

        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, IncludeFields = true };

        DiscountDTO discountDTO = JsonSerializer.Deserialize<DiscountDTO>(response, options)
            ?? throw new ArgumentNullException(nameof(response));

        return discountDTO.Discount;
    }

    public class DiscountDTO
    {
        public string Id { get; set; }
        public int Discount { get; set; }
    }
}