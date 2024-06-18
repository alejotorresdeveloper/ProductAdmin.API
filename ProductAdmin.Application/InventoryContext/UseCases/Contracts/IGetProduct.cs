namespace ProductAdmin.Application.InventoryContext.UseCases.Contracts;

using ProductAdmin.Domain.InventoryContext.Product;

public interface IGetProduct
{
    Product ExecuteAsync(int productId);
}