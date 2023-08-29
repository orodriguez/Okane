using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;

namespace Okane.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService) =>
        _categoriesService = categoriesService;
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
    public ActionResult<CategoryResponse> Post(CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return _categoriesService.Register(request);
    }
    
    [HttpGet]
    public IEnumerable<CategoryResponse> Get() => 
        _categoriesService.Retrieve();
}