using Microsoft.AspNetCore.Mvc;
using Okane.Contracts;

namespace Okane.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService) => 
        _expensesService = expensesService;

    [HttpPost]
    public ExpenseResponse Post(CreateExpenseRequest request) => 
        _expensesService.Register(request);
    
    [HttpGet]
    public IEnumerable<ExpenseResponse> Get(string? category) => 
        _expensesService.Retrieve(category);
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ById(int id)
    {
        var response = _expensesService.ById(id);

        if (response == null)
            return NotFound();
        
        return Ok(response);
    }
}