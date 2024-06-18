namespace ProductAdmin.Application.Test.InventoryContext.UseCases;

using Moq;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.TestData.Inventory;

public class GetProductUseCaseTests
{
    private MockRepository _mockRepository;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public GetProductUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _productRepositoryMock = _mockRepository.Create<IProductRepository>();
    }

    private GetProductUseCase CreateGetProductUseCase()
    {
        return new GetProductUseCase(_productRepositoryMock.Object);
    }

    [Fact]
    public void ExecuteAsync_GetProductById_FromRepository()
    {
        // Arrange
        Product product = ProductMother.Created();

        _productRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).Returns(product);

        var getProductUseCase = CreateGetProductUseCase();
        int productId = 1;

        // Act
        var result = getProductUseCase.ExecuteAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.ProductId, result.ProductId);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Stock, result.Stock);
        Assert.Equal(product.Discount, result.Discount);
        Assert.Equal(product.Category, result.Category);
        Assert.Equal(product.CreateDate, result.CreateDate);
        Assert.Equal(product.UpdateDate, result.UpdateDate);
        Assert.Equal(product.FinalPrice, result.FinalPrice);
        Assert.Equal(product.StatusName, result.StatusName);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public void ExecuteAsync_GetProductById_ThrowInventoryContextExceptionNoExistsProduct()
    {
        // Arrange
        Product? product = null;

        _productRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).Returns(product);

        var getProductUseCase = CreateGetProductUseCase();
        int productId = 1;

        // Act
        var result = Assert.ThrowsAny<InventoryContextException>(() => getProductUseCase.ExecuteAsync(productId));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<InventoryContextException>(result);
        Assert.Equal((int)InventoryContextExceptionEnum.NoExistsProduct, result.Code);
        Assert.Equal("Product does not exist", result.Message);
        _productRepositoryMock.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        _mockRepository.VerifyAll();
    }
}
