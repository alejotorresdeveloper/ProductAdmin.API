namespace ProductAdmin.Application.InventoryContext.UseCases.Contracts;

using ProductAdmin.Domain.InventoryContext.Product;

public interface IGetProducts
{
    IEnumerable<Product> ExecuteAsync();
}