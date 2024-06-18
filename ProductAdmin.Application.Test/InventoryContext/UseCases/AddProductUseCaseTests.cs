namespace ProductAdmin.Application.Test.InventoryContext.UseCases;

using Moq;
using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.Domain.SharedKernel;
using ProductAdmin.TestData.Inventory;
using System.Linq.Expressions;

public class AddProductUseCaseTests
{
    private MockRepository _mockRepository;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly Mock<IDiscountService> _mockDiscountService;



    public AddProductUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _productRepositoryMock = _mockRepository.Create<IProductRepository>();
        _mockCategoryRepository = _mockRepository.Create<ICategoryRepository>();
        _mockDiscountService = _mockRepository.Create<IDiscountService>();


    }

    private AddProductUseCase CreateAddProductUseCase()
    {
        List<Status> statusList = [Status.BuildInactive(), Status.BuildActive()];
        return new AddProductUseCase(_productRepositoryMock.Object,
                                     _mockCategoryRepository.Object,
                                     _mockDiscountService.Object,
                                     statusList);
    }

    [Fact]
    public async Task ExecuteAsync_AddProductIntoTheRepository()
    {
        // Arrange
        int categoryId = 1;
        int discount = 10;
        int lastId = 1;
        Category category = CategoryMother.Created(categoryId);
        List<Product>? products = null;

        _productRepositoryMock.Setup(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);
        _productRepositoryMock.Setup(p => p.LastId()).Returns(lastId);
        _productRepositoryMock.Setup(p => p.Add(It.IsAny<Product>())).ReturnsAsync(true);
        _mockCategoryRepository.Setup(c => c.GetById(It.IsAny<int>())).Returns(category);
        _mockDiscountService.Setup(d => d.GetDiscount(It.IsAny<int>())).ReturnsAsync(discount);

        var addProductUseCase = CreateAddProductUseCase();
        AddProductDTO product = new AddProductDTO
        {
            Name = "Product 1",
            Description = "Description",
            Price = 100,
            Stock = 10,
            CategoryId = categoryId
        };

        // Act
        var result = await addProductUseCase.ExecuteAsync(
            product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(lastId, result.ProductId);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Stock, result.Stock);
        Assert.Equal(discount, result.Discount);
        Assert.Equal(category, result.Category);
        Assert.Equal(result.FinalPrice, result.Price - (result.Price * discount / 100));
        _productRepositoryMock.Verify(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ExecuteAsync_ThrowInventoryContextExceptionExistProduct()
    {
        // Arrange
        List<Product> products = ProductMother.GetProducts();

        _productRepositoryMock.Setup(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);

        var addProductUseCase = CreateAddProductUseCase();
        AddProductDTO product = new AddProductDTO
        {
            Name = "Nike Shoes",
            Description = "Description",
            Price = 100,
            Stock = 10,
            CategoryId = 1
        };

        // Act
        var result = await Assert.ThrowsAnyAsync<InventoryContextException>(() => addProductUseCase.ExecuteAsync(product));

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)InventoryContextExceptionEnum.ExistProduct, result.Code);
        Assert.Equal("Product already exists", result.Message);
        _productRepositoryMock.Verify(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ExecuteAsync_ThrowInventoryContextExceptionNoExistCategory()
    {
        // Arrange
        List<Product>? products = null;
        Category? category = null;

        _productRepositoryMock.Setup(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);
        _mockCategoryRepository.Setup(c => c.GetById(It.IsAny<int>())).Returns(category);

        var addProductUseCase = CreateAddProductUseCase();
        AddProductDTO product = new AddProductDTO
        {
            Name = "Nike Shoes",
            Description = "Description",
            Price = 100,
            Stock = 10,
            CategoryId = 1
        };

        // Act
        var result = await Assert.ThrowsAnyAsync<InventoryContextException>(() => addProductUseCase.ExecuteAsync(product));

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)InventoryContextExceptionEnum.NoExistCategory, result.Code);
        Assert.Equal("Category does not exist", result.Message);
        _productRepositoryMock.Verify(p => p.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockCategoryRepository.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
        _mockRepository.VerifyAll();
    }
}
