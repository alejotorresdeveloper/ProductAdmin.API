namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;

public class AddProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public int? CategoryId { get; set; }
}