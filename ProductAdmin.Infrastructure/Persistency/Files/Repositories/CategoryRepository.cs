namespace ProductAdmin.Infrastructure.Persistency.Files.Repositories;

using Microsoft.Extensions.Configuration;
using ProductAdmin.Application.InventoryContext.Repositories;
using ProductAdmin.Domain.InventoryContext.Product;
using System.Linq;
using System.Linq.Expressions;

public class CategoryRepository(IConfiguration configuration) : ICategoryRepository
{
    private const string _fileNameKey = "Persistence:Files:Categories";

    private readonly string _folder = RepositoryExtensions.GetFolder();
    private readonly string _fileName = RepositoryExtensions.GetConfigurationValue(configuration, _fileNameKey);

    public Category GetById(int id)
    {
        List<Category> categories = RepositoryExtensions.GetData<Category>(FilePath());

        return categories.First(c => c.CategoryId == id);
    }

    public List<Category> GetCategories(Expression<Func<Category, bool>> predicate)
    {
        List<Category> categories = RepositoryExtensions.GetData<Category>(FilePath());

        return categories.Where(predicate.Compile()).ToList();
    }

    private string FilePath() => RepositoryExtensions.GetFilePath(_folder, _fileName);
}