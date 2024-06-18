namespace ProductAdmin.Domain.InventoryContext.DTO;

public class EditProductDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public int? Discount { get; set; }

    public EditCategoryDTO? Category { get; set; }
}