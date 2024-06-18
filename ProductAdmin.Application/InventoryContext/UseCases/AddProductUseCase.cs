namespace ProductAdmin.Application.InventoryContext.UseCases;

using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Application.InventoryContext.UseCases.Contracts;
using ProductAdmin.Domain.InventoryContext;
using ProductAdmin.Domain.InventoryContext.DTO;
using ProductAdmin.Domain.InventoryContext.Product;
using ProductAdmin.Domain.SharedKernel;
using System;

public class AddProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository, IDiscountService discountService, List<Status> statuses) : IAddProduct
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IDiscountService _discountService = discountService;
    private readonly List<Status> _statuses = statuses;

    public async Task<Product> ExecuteAsync(AddProductDTO product)
    {
        List<Product>? existingProduct = _productRepository.GetProducts(existingProduct => existingProduct.Name == product.Name);

        if (existingProduct is not null && existingProduct.Any())
            throw new InventoryContextException(InventoryContextExceptionEnum.ExistProduct);

        Category category = _categoryRepository.GetById(product.CategoryId) ??
            throw new InventoryContextException(InventoryContextExceptionEnum.NoExistCategory);

        int productId = _productRepository.LastId();
        double statusId = GetRamdomStatusId();

        string statusName = _statuses.First(status => status.StatusId == statusId).Name;

        int discount = await _discountService.GetDiscount(productId);

        Product productToAdd = Product.Build(productId, product.Name, statusName, product.Description, product.Price, product.Stock, discount, category);

        await _productRepository.Add(productToAdd);

        return productToAdd;
    }

    private static double GetRamdomStatusId()
    {
        Random random = new Random();
        double statusId = random.Next(2) * random.Next(2);
        return statusId;
    }
}