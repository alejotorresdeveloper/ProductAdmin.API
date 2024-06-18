namespace ProductAdmin.Infrastructure.Persistency.Files.Repositories;

using Microsoft.Extensions.Configuration;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Domain.InventoryContext.Product;
using System.Linq;
using System.Linq.Expressions;

public class ProductRepository(IConfiguration configuration) : IProductRepository
{
    private const string _fileNameKey = "Persistence:Files:Products";

    private readonly string _folder = RepositoryExtensions.GetFolder();
    private readonly string _fileName = RepositoryExtensions.GetConfigurationValue(configuration, _fileNameKey);

    public async ValueTask<bool> Add(Product product)
    {
        List<Product> products = RepositoryExtensions.GetData<Product>(FilePath());

        products.Add(product);
        await RepositoryExtensions.SaveData(products, FilePath()).ConfigureAwait(false);
        return true;
    }

    public Product? GetById(int id)
    {
        List<Product> products = RepositoryExtensions.GetData<Product>(FilePath());

        return products.FirstOrDefault(p => p.ProductId == id);
    }

    public Product? GetProduct(Expression<Func<Product, bool>> predicate)
    {
        List<Product> products = RepositoryExtensions.GetData<Product>(FilePath());

        return products.FirstOrDefault(predicate: predicate.Compile());
    }

    public List<Product> GetProducts(Expression<Func<Product, bool>> predicate)
    {
        List<Product> products = RepositoryExtensions.GetData<Product>(FilePath());

        return products.Where(predicate.Compile()).ToList();
    }

    public async ValueTask<bool> Update(Product product)
    {
        List<Product> products = RepositoryExtensions.GetData<Product>(FilePath());

        int index = products.FindIndex(p => p.ProductId == product.ProductId);
        products[index] = product;

        await RepositoryExtensions.SaveData(products, FilePath()).ConfigureAwait(false);
        return true;
    }

    public int LastId() => RepositoryExtensions.LastId<Product>(FilePath());

    private string FilePath() => RepositoryExtensions.GetFilePath(_folder, _fileName);
}