using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly OkaneDbContext _db;

    public CategoriesRepository(OkaneDbContext db) => _db = db;

    public Category? ByName(string categoryName) => 
        _db.Categories.FirstOrDefault(category => category.Name == categoryName);

    public void Add(Category category)
    {
        _db.Categories.Add(category);
        _db.SaveChanges();
    }
}