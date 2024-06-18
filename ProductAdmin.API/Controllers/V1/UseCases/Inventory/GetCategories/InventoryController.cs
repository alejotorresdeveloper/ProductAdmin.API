namespace ProductAdmin.API.Controllers.V1.UseCases.Inventory.GetCategories;

using Microsoft.AspNetCore.Mvc;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Responses;
using ProductAdmin.API.Middleware;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using System.Net.Mime;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]
public class InventoryController(IGetCategories useCase) : ControllerBase
{
    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<IEnumerable<CategoryResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
    public IActionResult GetCategory()
    {
        var categories = useCase.ExecuteAsync();
        return Ok(CustomResponse<IEnumerable<CategoryResponse>>.BuildSuccess(categories.Select(category => new CategoryResponse
        {
            Id = category.CategoryId,
            Name = category.Name
        })));
    }
}