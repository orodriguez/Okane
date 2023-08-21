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
    public IEnumerable<ExpenseResponse> Get() => 
        _expensesService.RetrieveAll();
}