namespace ProductAdmin.Application.InventoryContext.UseCases.Contracts;

using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;

public interface IAddProduct
{
    Task<Product> ExecuteAsync(AddProductDTO product);
}