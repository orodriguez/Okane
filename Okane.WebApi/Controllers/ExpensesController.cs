using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Services;

namespace Okane.WebApi.Controllers;

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
    public ActionResult<ExpenseResponse> Post(CreateExpenseRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!Uri.TryCreate(request.InvoiceUrl, UriKind.Absolute, out var _))
        {
            ModelState.AddModelError("InvoiceUrl", "Invalid URL format");
            return BadRequest(ModelState);
        }
        
        var newExpense = new CreateExpenseRequest
        {

            InvoiceUrl = request.InvoiceUrl,
            CreatedDate = DateTime.UtcNow,
            Category = null
        };

        var registeredExpense = _expensesService.Register(newExpense);

        return Ok(registeredExpense);
    }

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
    
    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expenseToDelete = _expensesService.ById(id);

        if (expenseToDelete == null)
        {
            return NotFound(); 
        }

        _expensesService.Delete(id); 
        return NoContent(); 
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateExpense(int id, UpdateExpenseRequest request)
    {
        var expenseToUpdate = _expensesService.ById(id);

        if (expenseToUpdate == null)
        {
            return NotFound(); 
        }

       
        expenseToUpdate.Category = request.Category;
        expenseToUpdate.Amount = request.Amount;
      

        _expensesService.Update(expenseToUpdate); 
        return NoContent();
    }

}

