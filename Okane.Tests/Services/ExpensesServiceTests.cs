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
        var expense = _expensesService.Register(new Expense {
            Category = "Groceries",
            Amount = 10
        });
        
        Assert.Equal(1, expense.Id);
        Assert.Equal(10, expense.Amount);
        Assert.Equal("Groceries", expense.Category);
    }
    
    [Fact]
    public void RetrieveAllExpenses() {
        _expensesService.Register(new Expense {
            Category = "Groceries",
            Amount = 10
        });

        _expensesService.Register(new Expense {
            Category = "Entertainment",
            Amount = 20
        });

        var allExpenses = _expensesService.RetrieveAll();

        Assert.Equal(2, allExpenses.Count());
    }
}
