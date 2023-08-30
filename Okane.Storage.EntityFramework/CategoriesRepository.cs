using Microsoft.EntityFrameworkCore;
using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly OkaneDbContext _db;

    public CategoriesRepository(OkaneDbContext db) => _db = db;


    public IEnumerable<Category> ByCategory(string category) =>
        _db.Categories.Include(categories => categories.Name == category);
    

    public Category? ByName(string categoryName) => 
        _db.Categories.FirstOrDefault(category => category.Name == categoryName);

    public void Add(Category category)
    {
        _db.Categories.Add(category);
        _db.SaveChanges();
    }

    public IEnumerable<Category> All() =>
        _db.Categories.Include(category => category.Expenses);
    

    public Category? ById(int id) =>
    _db.Categories
        .Include(category => category.Expenses)
    .FirstOrDefault(category => category.Id == id);
}