namespace ProductAdmin.TestData.Inventory;

using ProductAdmin.Domain.InventoryContext.Product;

public static class ProductMother
{
    public static Product Created(int productId = 1,
               string name = "Adidas T-Shirt",
               string statusName = "Active",
               string description = "Adidas T-Shirt Unisex",
               decimal price = 25.00M,
               int stock = 15,
               int discount = 10,
               Category? category = null)
    {
        category ??= CategoryMother.Created();

        return Product.Build(productId, name, statusName, description, price, stock, discount, category);

    }

    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            Created(),
            Created(productId: 2,
                    name: "Nike Shoes",
                    statusName: "Active",
                    description: "Nike Shoes for running",
                    price: 50.00M,
                    stock: 10,
                    discount: 5,
                    category: CategoryMother.Created(1, "Footwear", "Shoes and sandals")),
            Created(productId: 3,
                    name: "Samsung Galaxy S21",
                    statusName: "Active",
                    description: "Samsung Galaxy S21 5G",
                    price: 800.00M,
                    stock: 5,
                    discount: 0,
                    category: CategoryMother.Created(2, "Electronics", "Gadgets and devices")),
            Created(productId: 4,
                    name: "Apple MacBook Pro",
                    statusName: "Active",
                    description: "Apple MacBook Pro 13-inch",
                    price: 1200.00M,
                    stock: 3,
                    discount: 0,
                    category: CategoryMother.Created(2, "Electronics", "Gadgets and devices")),

        };
    }
}
