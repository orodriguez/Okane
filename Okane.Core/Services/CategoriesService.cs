using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Validations;

namespace Okane.Core.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categories;
    private readonly IValidator<CreateCategoryRequest> _validator;

    public CategoriesService(ICategoriesRepository categoriesRepository,IValidator<CreateCategoryRequest> validator)
    {
        _categories = categoriesRepository;
        _validator = validator;
    }
    
    
    public (CategoryResponse?, Errors?) Register(CreateCategoryRequest request)
    {
        var errors = _validator.Validate(request);
        
        if (errors.Any())
            return (null, errors);
        
        var category = new Category
        {
            
        };

        _categories.Add(category);

        return (CategoryResponse(category), null);
    }

    
    public IEnumerable<CategoryResponse> Retrieve(string? category = null)
    {
        var result = category == null ? _categories.All() : _categories.ByCategory(category);

        return result
            .Select(CategoryResponse);
    }

    public CategoryResponse? ById(int id)
    {
        var category = _categories.ById(id);

        return CategoryResponse(category);
    }
    
    private static CategoryResponse CategoryResponse(Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name,
            Expenses = null
            
        };
}