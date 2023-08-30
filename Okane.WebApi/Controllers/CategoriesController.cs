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
    
    public ActionResult<CategoryResponse> Post(CreateCategoryRequest request) =>
        _categoriesService.Register(request)switch
        {
            ({ } category, null) => Ok(category),
            (null, {} errors) => BadRequest(errors),
            _ => throw new ArgumentOutOfRangeException()
        };
    
    [HttpGet]
    public IEnumerable<CategoryResponse> Get(string? category) => 
        _categoriesService.Retrieve(category);
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ById(int id) => 
        _categoriesService.ById(id) != null ? Ok(_categoriesService.ById(id)) : NotFound();
}