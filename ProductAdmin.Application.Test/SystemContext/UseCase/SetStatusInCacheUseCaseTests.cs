using Microsoft.Extensions.Configuration;
using Moq;
using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Application.SystemContext.UseCase;
using ProductAdmin.Domain.SharedKernel;

namespace ProductAdmin.Application.Test.SystemContext.UseCase;

public class SetStatusInCacheUseCaseTests
{
    private MockRepository _mockRepository;
    private readonly Mock<ICacheRepository> _cacheRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public SetStatusInCacheUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Loose);
        _cacheRepositoryMock = _mockRepository.Create<ICacheRepository>();
        _configurationMock = GetConfigurationMock();
    }

    [Fact]
    public void ExecuteAsync_ReturnDefaultStatusesList_SinceThereIsNothingInTheCache()
    {
        // Arrange
        string cacheKey = "statuses";
        _cacheRepositoryMock.Setup(c => c.GetMemoryValue<List<Status>>(cacheKey)).Returns((List<Status>)null);

        var setStatusInCacheUseCase = new SetStatusInCacheUseCase(_cacheRepositoryMock.Object, _configurationMock.Object);

        // Act
        var result = setStatusInCacheUseCase.ExecuteAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].StatusId);
        Assert.Equal("Active", result[0].Name);
        Assert.Equal(0, result[1].StatusId);
        Assert.Equal("Inactive", result[1].Name);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public void ExecuteAsync_ReturnStatusesList_StoredInTheCache()
    {
        // Arrange
        List<Status> statuses = new() { Status.BuildInactive() };
        string cacheKey = "statuses";
        _cacheRepositoryMock.Setup(c => c.GetMemoryValue<List<Status>>(cacheKey)).Returns(statuses);

        var setStatusInCacheUseCase = new SetStatusInCacheUseCase(_cacheRepositoryMock.Object, _configurationMock.Object);

        // Act
        var result = setStatusInCacheUseCase.ExecuteAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(1, result.Count);
        Assert.Equal(0, result[0].StatusId);
        Assert.Equal("Inactive", result[0].Name);
        _mockRepository.VerifyAll();
    }

    private Mock<IConfiguration> GetConfigurationMock()
    {
        var mock = _mockRepository.Create<IConfiguration>();

        // Request query params
        mock.Setup(c => c.GetSection("Cache:StatusKey").Value).Returns("statuses");
        return mock;
    }
}
