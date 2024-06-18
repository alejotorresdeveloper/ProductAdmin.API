namespace ProductAdmin.Infrastructure.Test.HttpClients.DiscountAPI;

using Microsoft.Extensions.Configuration;
using Moq;
using ProductAdmin.Infrastructure.HttpClients.DiscountAPI;
using ProductAdmin.Infrastructure.Test.HttpClients.Helper;
using System.Net;

public class DiscountServiceTests
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;

    public DiscountServiceTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Loose);
        _mockConfiguration = GetConfigurationMock();
        _mockHttpClientFactory = _mockRepository.Create<IHttpClientFactory>();
    }

    private Mock<IConfiguration> GetConfigurationMock()
    {
        var mock = _mockRepository.Create<IConfiguration>();

        // Request query params
        mock.Setup(c => c.GetSection("ExternalsAPIs:DiscountAPI:Resource").Value).Returns("productDiscount");
        return mock;
    }

    private DiscountService CreateDiscountService()
    {
        return new DiscountService(_mockConfiguration.Object,
                                   _mockHttpClientFactory.Object);
    }

    [Fact]
    public async Task GetDiscount_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        string discountAPI = "https://discount.api";
        
        var mockHandler = new MockHttpMessageHandler(@"{""discount"": 20,""id"": ""10""}", HttpStatusCode.OK);
        var client = new HttpClient(mockHandler);

        client.BaseAddress = new Uri(discountAPI);

        _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

        var service = CreateDiscountService();
        int productId = 1;

        // Act
        var result = await service.GetDiscount(productId);

        // Assert
        Assert.IsType<int>(result);
        Assert.Equal(20, result);
    }
}
