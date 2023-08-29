using Okane.Core.Entities;
using Okane.Core.Services;

namespace Okane.Core.Repositories;

public interface ICategoriesRepository
{
    Category ByName(string categoryName);
    void Add(Category category);
    IEnumerable<Category> All();
}