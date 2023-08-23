using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Services;

namespace Okane.Tests.Services;

public class ExpensesServiceTests
{
    private readonly ExpensesService _expensesService;

    public ExpensesServiceTests()
    {
        _expensesService = new ExpensesService(new InMemoryExpensesRepository());
    }

    [Fact]
    public void RegisterExpense()
    {
        var expense = _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10,
            Description = "Test"
        });
        
        Assert.Equal(1, expense.Id);
        Assert.Equal(10, expense.Amount);
        Assert.Equal("Groceries", expense.Category);
        Assert.Equal("Test", expense.Description );
    }
    
    [Fact]
    public void RetrieveAllExpenses() {
        _expensesService.Register(new CreateExpenseRequest {
            Category = "Groceries",
            Amount = 10,
            Description = "Test"
        });

        _expensesService.Register(new CreateExpenseRequest {
            Category = "Entertainment",
            Amount = 20,
            Description = "Test"
        });

        var allExpenses = _expensesService.RetrieveAll();

        Assert.Equal(2, allExpenses.Count());
    }
}
