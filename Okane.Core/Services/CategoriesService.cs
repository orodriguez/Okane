using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categories;

    public CategoriesService(ICategoriesRepository categories)
    {
        _categories = categories;
    }


    public CategoryResponse Register(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name
        };
        
        _categories.Add(category);

        return CreateResponse(category);
    }

    public IEnumerable<CategoryResponse> Retrieve()
    {
        var result = _categories.All();

        return result
            .Select(CreateResponse);
    }
    
    private static CategoryResponse CreateResponse(Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name
        };
}