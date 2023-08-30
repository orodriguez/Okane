using System.Collections.Generic;
using System.Linq;
using Okane.Contracts;
using Okane.Core.Repositories;

namespace Okane.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public CategoryResponse Create(CreateCategoryRequest request)
        {
            // Implement validation and logic for category creation here
            // You can also check if the category name is one of the allowed categories.
            // For simplicity, this example assumes all categories are valid.
            return _categoriesRepository.Add(request);
        }

        public IEnumerable<CategoryResponse> GetAll()
        {
            return _categoriesRepository.GetAll();
        }
    }
}
