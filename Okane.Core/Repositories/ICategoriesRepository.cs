using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface ICategoriesRepository
{
    Category? ByName(string categoryName);
    void Add(Category category);
}