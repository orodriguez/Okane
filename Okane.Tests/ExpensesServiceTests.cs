namespace Okane.Tests;

public class ExpensesServiceTests
{
    [Fact]
    public void RegisterExpense()
    {
        var expensesRepository = new InMemoryExpensesRepository();
        
        var expensesService = new ExpensesService(expensesRepository);
        
        var expense = expensesService.RegisterExpense(new Expense {
            Category = "Groceries",
            Amount = 10
        });
        
        Assert.Equal(1, expense.Id);
        Assert.Equal(10, expense.Amount);
        Assert.Equal("Groceries", expense.Category);
    }
}
