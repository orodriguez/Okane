namespace Okane.Tests;

public class ExpensesService
{
    private readonly IExpensesRepository _expenses;

    public ExpensesService(IExpensesRepository expenses) =>
        _expenses = expenses;

    public Expense RegisterExpense(Expense expense)
    {
        _expenses.Add(expense);
        return expense;
    }
}
