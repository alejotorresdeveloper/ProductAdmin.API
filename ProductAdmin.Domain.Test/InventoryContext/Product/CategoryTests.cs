namespace Product;

using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;

public class CategoryTests
{
    [Fact]
    public void Category_Build_CreatesANewIntanceOfCategoryClass()
    {
        // Arrange
        int categoryId = 1;
        string name = "Test";
        string description = "Category for unit test";

        // Act
        Category result = Category.Build(categoryId,
                                    name,
                                    description);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.CategoryId, categoryId);
        Assert.Equal(result.Name, name);
        Assert.Equal(result.Description, description);
    }
    [Fact]
    public void Category_Update_UpdateOnlyDescriptionValue()
    {
        // Arrange
        int categoryId = 1;
        string name = "Test";
        string description = "Category for unit test";
        Category category = Category.Build(categoryId,
                                    name,
                                    description);

        // Act
        EditCategoryDTO editCategory = new EditCategoryDTO { Description = "Category for unit test update" };
        category = category.Update(editCategory);

        // Assert
        Assert.NotNull(category);
        Assert.Equal(category.CategoryId, categoryId);
        Assert.Equal(category.Name, name);
        Assert.Equal(category.Description, editCategory.Description);
    }
}
