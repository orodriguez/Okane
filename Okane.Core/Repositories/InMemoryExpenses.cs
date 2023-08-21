using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private readonly IList<Expense> _expenses;
    private int _nextId = 1;
    public InMemoryExpensesRepository() : this(new List<Expense>())
    {
    }

    private InMemoryExpensesRepository(IList<Expense> expenses) =>
        _expenses = expenses;

    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> All() => _expenses;
}