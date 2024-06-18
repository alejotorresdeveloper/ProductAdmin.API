namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.GetProducts;

using Microsoft.AspNetCore.Mvc;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;
using ProductAdmin.API.Middleware;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext.Product;

public class InventoryController(IGetProducts useCase) : ControllerBase
{
    [HttpGet("products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<IEnumerable<GetProductResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
    public IActionResult GetProductsAsync()
    {
        IEnumerable<Product> products = useCase.ExecuteAsync();
        return Ok(CustomResponse<IEnumerable<GetProductResponse>>.BuildSuccess(products.Select(product => new GetProductResponse
        {
            Id = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Discount = product.Discount,
            FinalPrice = product.FinalPrice,
            Category = new CategoryResponse
            {
                Id = product.Category.CategoryId,
                Name = product.Category.Name
            }
        })));
    }
}