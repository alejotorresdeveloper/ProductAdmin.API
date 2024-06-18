namespace ProductAdmin.TestData.Inventory;

using ProductAdmin.Domain.InventoryContext.Product;

public static class CategoryMother
{
    public static Category Created(int categoryId = 1, string name = "Clothing", string description = "Apparel for men, women, and children")
    {
        return Category.Build(categoryId, name, description);
    }

    public static List<Category> GetCategories()
    {
        return new List<Category>
        {
            Created(),
            Created(1, "Electronics", "Gadgets and devices"),
            Created(2, "Home", "Furniture and home decor"),
            Created(3, "Books", "Literature and non-fiction"),
        };
    }
}
