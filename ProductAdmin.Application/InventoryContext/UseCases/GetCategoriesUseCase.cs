namespace ProductAdmin.Application.InventoryContext.UseCases;

using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext.Product;

public class GetCategoriesUseCase(ICategoryRepository categoryRepository) : IGetCategories
{
    public IEnumerable<Category> ExecuteAsync() =>
        categoryRepository.GetCategories(_ => true);
}