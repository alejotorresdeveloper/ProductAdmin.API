namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.GetProduct;

using Microsoft.AspNetCore.Mvc;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;
using ProductAdmin.API.Middleware;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext.Product;
using System.ComponentModel.DataAnnotations;

public class InventoryController(IGetProduct useCase) : ControllerBase
{
    [HttpGet("product")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<GetProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
    public IActionResult GetProductAsync([Required][FromQuery] int id)
    {
        Product product = useCase.ExecuteAsync(id);
        return Ok(CustomResponse<GetProductResponse>.BuildSuccess(new GetProductResponse
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
        }));
    }
}