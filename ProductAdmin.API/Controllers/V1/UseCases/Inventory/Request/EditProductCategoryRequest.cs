namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;

public class EditProductCategoryRequest
{
    public int? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}