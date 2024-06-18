namespace ProductAdmin.Application.Test.InventoryContext.UseCases;

using Moq;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.TestData.Inventory;
using System.Linq.Expressions;

public class GetCategoriesUseCaseTests
{
    private MockRepository _mockRepository;

    private Mock<ICategoryRepository> _mockCategoryRepository;

    public GetCategoriesUseCaseTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _mockCategoryRepository = _mockRepository.Create<ICategoryRepository>();
    }

    private GetCategoriesUseCase CreateGetCategoriesUseCase()
    {
        return new GetCategoriesUseCase(_mockCategoryRepository.Object);
    }

    [Fact]
    public void ExecuteAsync_GetAllCategoriesInTheRepository()
    {
        // Arrange
        List<Category> categories = CategoryMother.GetCategories();
        _mockCategoryRepository.Setup(c => c.GetCategories(It.IsAny<Expression<Func<Category, bool>>>())).Returns(categories);

        var getCategoriesUseCase = CreateGetCategoriesUseCase();

        // Act
        var result = getCategoriesUseCase.ExecuteAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(categories.Count, result.Count());
        Assert.True(result.All(categories.Contains));
        _mockCategoryRepository.Verify(c => c.GetCategories(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
        _mockRepository.VerifyAll();
    }
}
