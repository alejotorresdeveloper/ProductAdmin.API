using ProductAdmin.Domain.InventoryContext.DTO;

namespace ProductAdmin.Domain.InventoryContext.Product;

public sealed class Category
{
    public int CategoryId { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Category(int categoryId, string name, string description)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }

    public Category Update(EditCategoryDTO category)
    {
        int categoryId = category.CategoryId ?? CategoryId;
        string name = category.Name ?? Name;
        string description = category.Description ?? Description;

        return new Category(categoryId, name, description);
    }

    public static Category Build(int categoryId, string name, string description)
        => new Category(categoryId, name, description);
}