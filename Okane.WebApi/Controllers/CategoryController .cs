using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;
using Okane.Services;

namespace Okane.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        public ActionResult<CategoryResponse> Post(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = _categoriesService.Create(request);

            return CreatedAtAction(nameof(Get), new { name = createdCategory.Name }, createdCategory);
        }

        [HttpGet]
        public IEnumerable<CategoryResponse> Get()
        {
            return _categoriesService.GetAll();
        }
    }
}
