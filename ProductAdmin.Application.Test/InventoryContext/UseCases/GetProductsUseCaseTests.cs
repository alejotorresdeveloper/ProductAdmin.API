namespace ProductAdmin.Application.Test.InventoryContext.UseCases;

using Moq;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.TestData.Inventory;
using System.Linq.Expressions;

public class GetProductsUseCaseTests
{
    private MockRepository _mockRepository;
    private readonly Mock<IProductRepository> _productRepositoryMock;


    public GetProductsUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _productRepositoryMock = _mockRepository.Create<IProductRepository>();
    }

    private GetProductsUseCase CreateGetProductsUseCase()
    {
        return new GetProductsUseCase(_productRepositoryMock.Object);
    }

    [Fact]
    public void ExecuteAsync_GetAllProductsInTheRepository()
    {
        // Arrange
        List<Product> products = ProductMother.GetProducts();

        _productRepositoryMock.Setup(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);

        var getProductsUseCase = CreateGetProductsUseCase();

        // Act
        var result = getProductsUseCase.ExecuteAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.All(products.Contains));
        _productRepositoryMock.Verify(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public void ExecuteAsync_ThrowInventoryContextExceptionNoExistsProducts()
    {
        // Arrange
        List<Product>? products = null;

        _productRepositoryMock.Setup(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);

        var getProductsUseCase = CreateGetProductsUseCase();

        // Act
        var result = Assert.ThrowsAny<InventoryContextException>(() => getProductsUseCase.ExecuteAsync());

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)InventoryContextExceptionEnum.NoExistsProducts, result.Code);
        Assert.Equal("Products do not exist", result.Message);
        _productRepositoryMock.Verify(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepository.VerifyAll();
    }
}
