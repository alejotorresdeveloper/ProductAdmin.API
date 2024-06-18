namespace ProductAdmin.Application.InventoryContext.Repositories;

using ProductAdmin.Domain.InventoryContext.Product;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface ICategoryRepository
{
    Category GetById(int id);

    List<Category> GetCategories(Expression<Func<Category, bool>> predicate);
}