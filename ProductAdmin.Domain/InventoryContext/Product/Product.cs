namespace ProductAdmin.Domain.InventoryContext.Product;

using ProductAdmin.Domain.InventoryContext.DTO;

public sealed record Product
{
    public Product(int productId,
                   string name,
                   string statusName,
                   string description,
                   decimal price,
                   decimal finalPrice,
                   int stock,
                   int discount,
                   Category category,
                   DateTime createDate,
                   DateTime updateDate)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        StatusName = statusName;
        Price = price;
        FinalPrice = finalPrice;
        Stock = stock;
        Discount = discount;
        Category = category;
        CreateDate = createDate;
        UpdateDate = updateDate;
    }

    public int ProductId { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public int Discount { get; private set; }
    public Category Category { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    public decimal FinalPrice { get; private set; }
    public string StatusName { get; private set; }

    public void Update(EditProductDTO productUpdate)
    {
        Name = productUpdate.Name ?? Name;
        Description = productUpdate.Description ?? Description;
        Price = productUpdate.Price.HasValue ? productUpdate.Price.Value : Price;
        Stock = productUpdate.Stock ?? Stock;
        Discount = productUpdate.Discount ?? Discount;
        Category = productUpdate.Category is not null ? Category.Update(productUpdate.Category) : Category;

        UpdateDate = DateTime.UtcNow;

        FinalPrice = Price * (100 - Discount) / 100;
    }

    public static Product Build(int productId,
                                string name,
                                string statusName,
                                string description,
                                decimal price,
                                int stock,
                                int discount,
                                Category category)
    {
        decimal finalPrice = price * (100 - discount) / 100;

        return new(productId: productId,
                      name: name,
                      statusName: statusName,
                      description: description,
                      price: price,
                      finalPrice: finalPrice,
                      stock: stock,
                      discount: discount,
                      category: category,
                      createDate: DateTime.UtcNow,
                      updateDate: DateTime.MinValue);
    }
}