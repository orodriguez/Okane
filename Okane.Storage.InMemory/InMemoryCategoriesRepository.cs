using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryCategoriesRepository : ICategoriesRepository
{
    private readonly List<Category> _list = new List<Category>();
    private int _nextId = 1;

    public Category ByName(string categoryName) => 
        _list.First(category => category.Name == categoryName);

    public void Add(Category category)
    {
        category.Id = _nextId++;
        _list.Add(category);
    }

    public IEnumerable<Category> All()
    {
        throw new NotImplementedException();
    }
}