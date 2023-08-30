using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface ICategoriesRepository
{
    IEnumerable<Category> GetAll();
    Category ByName(string categoryName);
    void Add(Category category);
}