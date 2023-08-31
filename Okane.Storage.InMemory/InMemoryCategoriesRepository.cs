using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryCategoriesRepository : ICategoriesRepository
{
    private readonly List<Category> _list = new List<Category>();
    private int _nextId = 1;

    public InMemoryCategoriesRepository()
    {
        _list.AddRange(new[]
        {
            new Category { Name = "Food" },
            new Category { Name = "Groceries" },
            new Category { Name = "Entertainment" },
            new Category { Name = "Taxes" },
        });
    }

    public Category? ByName(string categoryName) => 
        _list.FirstOrDefault(category => category.Name == categoryName);

    public void Add(Category category)
    {
        category.Id = _nextId++;
        _list.Add(category);
    }
}