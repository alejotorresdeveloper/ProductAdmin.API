namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;

public abstract class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public decimal FinalPrice { get; set; }
    public int Stock { get; set; }
}