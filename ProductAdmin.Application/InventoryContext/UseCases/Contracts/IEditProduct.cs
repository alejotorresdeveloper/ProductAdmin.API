using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;

namespace ProductAdmin.Application.InventoryContext.UseCases.Contracts;

public interface IEditProduct
{
    Task<Product> ExecuteAsync(int productId, EditProductDTO product);
}