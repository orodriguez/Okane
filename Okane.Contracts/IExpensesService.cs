using Okane.Contracts;

namespace Okane.Contracts
{
    public interface ICategoriesService
    {
        CategoryResponse Create(CreateCategoryRequest request);
        IEnumerable<CategoryResponse> GetAll();
    }
}
