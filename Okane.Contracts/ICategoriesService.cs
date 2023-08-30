namespace Okane.Contracts;

public interface ICategoriesService
{
    (CategoryResponse?, Errors?) Register(CreateCategoryRequest request);
    
    IEnumerable<CategoryResponse> Retrieve(string? category = null);
    
    CategoryResponse? ById(int id);
}