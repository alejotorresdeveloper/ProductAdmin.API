namespace ProductAdmin.Application.InventoryContext.Repositories;

using ProductAdmin.Domain.InventoryContext.Product;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface IProductRepository
{
    ValueTask<bool> Add(Product product);

    Product? GetById(int id);

    Product? GetProduct(Expression<Func<Product, bool>> predicate);

    List<Product>? GetProducts(Expression<Func<Product, bool>> predicate);

    int LastId();

    ValueTask<bool> Update(Product product);
}