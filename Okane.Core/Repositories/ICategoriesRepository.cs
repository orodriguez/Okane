using Okane.Contracts;
using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface ICategoriesRepository
{
    IEnumerable<Category> ByCategory(string category);
    Category? ByName(string categoryName);
    void Add(Category category);
    IEnumerable<Category> All();
    Category? ById(int id);
}