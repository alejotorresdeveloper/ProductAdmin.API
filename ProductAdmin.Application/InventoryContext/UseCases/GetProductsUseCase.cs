namespace ProductAdmin.Application.InventoryContext.UseCases;

using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.Product;

public class GetProductsUseCase(IProductRepository productRepository) : IGetProducts
{
    public IEnumerable<Product> ExecuteAsync()
        => productRepository.GetProducts(_ => true) ?? throw new InventoryContextException(InventoryContextExceptionEnum.NoExistsProducts);
}