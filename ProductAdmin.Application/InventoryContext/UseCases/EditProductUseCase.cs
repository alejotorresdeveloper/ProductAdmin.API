namespace ProductAdmin.Application.InventoryContext.UseCases;

using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;

public class EditProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository) : IEditProduct
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Product> ExecuteAsync(int productId, EditProductDTO product)
    {
        Product existingProduct = _productRepository.GetById(productId)
            ?? throw new InventoryContextException(InventoryContextExceptionEnum.NoExistsProduct);

        int categoryIdToUpdate = product.Category?.CategoryId ?? 0;

        if (product.Category is not null && categoryIdToUpdate != existingProduct.Category.CategoryId)
        {
            Category category = _categoryRepository.GetById(categoryIdToUpdate)
                ?? throw new InventoryContextException(InventoryContextExceptionEnum.NoExistCategory);

            product.Category = new EditCategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        existingProduct.Update(product);

        await _productRepository.Update(existingProduct).ConfigureAwait(false);

        return existingProduct;
    }
}