using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;
using ProductAdmin.API.Middleware;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.AddProduct;

[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]
public class InventoryController(IAddProduct addProduct, IValidator<AddProductRequest> validator) : ControllerBase
{
    [HttpPost("addProduct")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomResponse<AddProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> AddProduct([Required][FromBody] AddProductRequest request)
    {
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            validationResult.Errors.ForEach(error => ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
            return BadRequest(ModelState);
        }

        Product product = await addProduct.ExecuteAsync(new AddProductDTO
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price.Value,
            Stock = request.Stock.Value,
            CategoryId = request.CategoryId.Value
        });

        return Ok(CustomResponse<AddProductResponse>.BuildSuccess(new AddProductResponse
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