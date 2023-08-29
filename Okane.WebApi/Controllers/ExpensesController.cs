using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;

namespace Okane.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService) => 
        _expensesService = expensesService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
    public ActionResult<ExpenseResponse> Post(CreateExpenseRequest request) =>
        _expensesService.Register(request) switch
        {
            ({ } expense, null) => Ok(expense),
            (null, {} errors) => BadRequest(errors),
            _ => throw new ArgumentOutOfRangeException()
        };
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
    public ActionResult<ExpenseResponse> Update([FromRoute]int id, UpdateExpenseRequest request) =>
        _expensesService.Update(id, request) switch
        {
            ({ } expense, null) => Ok(expense),
            (null, EntityNotFoundErrors) => NotFound(),
            (null, { } errors) => BadRequest(errors),
            _ => throw new ArgumentOutOfRangeException()
        };

    [HttpGet]
    public IEnumerable<ExpenseResponse> Get(string? category)
    {
        return _expensesService.Retrieve(category);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ById(int id) => 
        _expensesService.ById(id) != null ? Ok(_expensesService.ById(id)) : NotFound();

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id) => 
        _expensesService.Delete(id) ? NoContent() : NotFound();
}