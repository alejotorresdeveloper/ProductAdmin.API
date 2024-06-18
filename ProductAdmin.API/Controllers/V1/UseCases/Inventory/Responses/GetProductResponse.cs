namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;

public class GetProductResponse : ProductResponse
{
    public CategoryResponse Category { get; set; }
}