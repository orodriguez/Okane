namespace Okane.Contracts;

public interface ICategoriesService
{
    CategoryResponse Register(CreateCategoryRequest request);
    IEnumerable<CategoryResponse> Retrieve();
}