namespace ProductAdmin.Application.InventoryContext.UseCases;

using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.Product;

public class GetProductUseCase(IProductRepository productRepository) : IGetProduct
{
    private readonly IProductRepository _productRepository = productRepository;

    public Product ExecuteAsync(int productId)
        => _productRepository.GetById(productId) ?? throw new InventoryContextException(InventoryContextExceptionEnum.NoExistsProduct);
}