using Microsoft.AspNetCore.Mvc;
using Okane.Core;
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
    public Expense Post(Expense expense) => 
        _expensesService.Register(expense);
    
    [HttpGet]
    public IEnumerable<Expense> Get() => 
        _expensesService.RetrieveAll();
}