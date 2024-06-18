namespace Product;

using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;

public class ProductTests
{
    [Fact]
    public void Prodcut_Update_OnlyUpdateDiscount()
    {
        // Arrange
        int productId = 1;
        string name = "Product test";
        string statusName = "Active";
        string description = "Product test";
        decimal price = 10;
        int stock = 5;
        int discount = 10;
        Category category = Category.Build(1, "Unit Test", "Category for unit tests");
        Product product = Product.Build(productId,
                                        name,
                                        statusName,
                                        description,
                                        price,
                                        stock,
                                        discount,
                                        category);
        EditProductDTO productUpdate = new EditProductDTO { Discount = 15 };

        //Act
        product.Update(productUpdate);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(product.Discount, productUpdate.Discount);
        Assert.Equal(product.ProductId, productId);
        Assert.Equal(product.Name, name);
        Assert.Equal(product.StatusName, statusName);
        Assert.Equal(product.Description, description);
        Assert.Equal(product.Price, price);
        Assert.Equal(product.Stock, stock);
        Assert.Equal(product.Category, category);
    }

    [Fact]
    public void Product_Build_CreatesANewIntaceOfProductClass()
    {
        // Arrange
        int productId = 1;
        string name = "Product test";
        string statusName = "Active";
        string description = "Product test";
        decimal price = 10;
        int stock = 5;
        int discount = 10;
        Category category = Category.Build(1, "Unit Test", "Category for unit tests");

        // Act
        var product = Product.Build(productId,
                                    name,
                                    statusName,
                                    description,
                                    price,
                                    stock,
                                    discount,
                                    category);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(product.Discount, discount);
        Assert.Equal(product.ProductId, productId);
        Assert.Equal(product.Name, name);
        Assert.Equal(product.StatusName, statusName);
        Assert.Equal(product.Description, description);
        Assert.Equal(product.Price, price);
        Assert.Equal(product.Stock, stock);
        Assert.Equal(product.Category, category);
    }
}
