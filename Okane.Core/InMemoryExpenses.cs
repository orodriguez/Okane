namespace Okane.Tests;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private readonly IList<Expense> _expenses;
    private int _nextId = 1;
    public InMemoryExpensesRepository() : this(new List<Expense>())
    {
    }

    public InMemoryExpensesRepository(IList<Expense> expenses) =>
        _expenses = expenses;

    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }
}