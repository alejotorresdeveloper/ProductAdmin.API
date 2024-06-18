namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;

public class EditProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public int? Discount { get; set; }

    public EditProductCategoryRequest? Category { get; set; }
}