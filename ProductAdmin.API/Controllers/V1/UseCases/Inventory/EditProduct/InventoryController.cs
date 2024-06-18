namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.EditProduct;

using Microsoft.AspNetCore.Mvc;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;
using ProductAdmin.API.Middleware;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]
public class InventoryController(IEditProduct useCase) : ControllerBase
{
    [HttpPut("editProduct")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomResponse<EditProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> AddProduct([Required][FromQuery] int id, [Required][FromBody] EditProductRequest request)
    {
        EditProductDTO editProductDTO = new()
        {
            Category = request.Category is null ? null : new EditCategoryDTO
            {
                CategoryId = request.Category.CategoryId.Value,
                Name = request.Category.Name,
                Description = request.Category.Description
            },
            Name = request.Name,
            Description = request.Description,
            Price = request.Price.HasValue ? request.Price.Value : null,
            Stock = request.Stock.HasValue ? request.Stock.Value : null,
            Discount = request.Discount.HasValue ? request.Discount.Value : null,
        };

        Product product = await useCase.ExecuteAsync(id, editProductDTO);

        return Ok(CustomResponse<EditProductResponse>.BuildSuccess(new EditProductResponse
        {
            Id = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Discount = product.Discount,
            FinalPrice = product.FinalPrice,
            Category = product.Category.Name
        }));
    }
}