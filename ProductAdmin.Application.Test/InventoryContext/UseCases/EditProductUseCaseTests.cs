namespace ProductAdmin.Application.Test.InventoryContext.UseCases;

using Moq;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.TestData.Inventory;

public class EditProductUseCaseTests
{
    private MockRepository _mockRepository;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;

    public EditProductUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _productRepositoryMock = _mockRepository.Create<IProductRepository>();
        _mockCategoryRepository = _mockRepository.Create<ICategoryRepository>();
    }

    private EditProductUseCase CreateEditProductUseCase()
    {
        return new EditProductUseCase(_productRepositoryMock.Object, _mockCategoryRepository.Object);
    }

    [Fact]
    public async Task ExecuteAsync_EditExistingProductOnlyNameAndDicountValues()
    {
        // Arrange
        Product product = ProductMother.Created();

        _productRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).Returns(product);
        _productRepositoryMock.Setup(p => p.Update(It.IsAny<Product>())).Returns(ValueTask.FromResult(true));

        var editProductUseCase = CreateEditProductUseCase();
        int productId = 1;
        EditProductDTO editProduct = new EditProductDTO { Name = $"{product.Name} modified", Discount = 16 };

        // Act
        var result = await editProductUseCase.ExecuteAsync(productId, editProduct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.ProductId, result.ProductId);
        Assert.Equal(editProduct.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Stock, result.Stock);
        Assert.Equal(editProduct.Discount, result.Discount);
        Assert.Equal(product.Category, result.Category);
        Assert.Equal(product.CreateDate, result.CreateDate);
        Assert.Equal(product.UpdateDate, result.UpdateDate);
        Assert.Equal(product.FinalPrice, result.FinalPrice);
        Assert.Equal(product.StatusName, result.StatusName);
        _productRepositoryMock.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ExecuteAsync_EditExistingProductWithCategory()
    {
        // Arrange
        Product product = ProductMother.Created();
        Category category = CategoryMother.Created(categoryId: 2, "Costumes");

        _productRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).Returns(product);
        _productRepositoryMock.Setup(p => p.Update(It.IsAny<Product>())).Returns(ValueTask.FromResult(true));
        _mockCategoryRepository.Setup(c => c.GetById(It.IsAny<int>())).Returns(category);

        var editProductUseCase = CreateEditProductUseCase();
        int productId = 1;
        EditProductDTO editProduct = new EditProductDTO
        {
            Name = $"{product.Name} modified",
            Discount = 16,
            Category = new EditCategoryDTO { CategoryId = category.CategoryId }
        };

        // Act
        var result = await editProductUseCase.ExecuteAsync(productId, editProduct);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.ProductId, result.ProductId);
        Assert.Equal(editProduct.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Stock, result.Stock);
        Assert.Equal(editProduct.Discount, result.Discount);
        Assert.NotEqual(category, result.Category);
        Assert.Equal(category.CategoryId, result.Category.CategoryId);
        Assert.Equal(category.Name, result.Category.Name);
        Assert.Equal(product.CreateDate, result.CreateDate);
        Assert.Equal(product.UpdateDate, result.UpdateDate);
        Assert.Equal(product.FinalPrice, result.FinalPrice);
        Assert.Equal(product.StatusName, result.StatusName);
        _productRepositoryMock.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        _mockRepository.VerifyAll();
    }
}
