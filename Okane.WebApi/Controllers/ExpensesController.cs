using Microsoft.AspNetCore.Mvc;
using Okane.Tests;

namespace Okane.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ExpensesService _expensesService;

    public ExpensesController(ExpensesService expensesService) => 
        _expensesService = expensesService;

    [HttpPost]
    public Expense Post(Expense expense) => 
        _expensesService.Register(expense);
    
    [HttpGet]
    public IEnumerable<Expense> Get() => 
        _expensesService.RetrieveAll();
}