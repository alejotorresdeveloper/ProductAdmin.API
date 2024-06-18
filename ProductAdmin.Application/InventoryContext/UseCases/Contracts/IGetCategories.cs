namespace ProductAdmin.Application.InventoryContext.UseCases.Contracts;

using ProductAdmin.Domain.InventoryContext.Product;

public interface IGetCategories
{
    IEnumerable<Category> ExecuteAsync();
}