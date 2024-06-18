namespace ProductAdmin.Domain.Test.InventoryContext;

using ProductAdmin.Domain.InventoryContext;

public class InventoryContextExceptionTests
{
    [Theory]
    [InlineData(InventoryContextExceptionEnum.ExistProduct, "Product already exists")]
    [InlineData(InventoryContextExceptionEnum.NoExistCategory, "Category does not exist")]
    [InlineData(InventoryContextExceptionEnum.NoExistsProduct, "Product does not exist")]
    [InlineData(InventoryContextExceptionEnum.NoExistsProducts, "Products do not exist")]
    [InlineData(default, "Unknown error")]
    public void TestMethod1(InventoryContextExceptionEnum exceptionEnum, string message)
    {
        // Act
        var inventoryContextException = new InventoryContextException(exceptionEnum);

        // Assert
        Assert.NotNull(inventoryContextException);
        Assert.Equal(inventoryContextException.Message, message);
        Assert.Equal(inventoryContextException.Code, (int)exceptionEnum);
    }
}
