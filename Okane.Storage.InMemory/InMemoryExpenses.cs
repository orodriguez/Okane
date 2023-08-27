using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private readonly List<Expense> _expenses;
    private int _nextId = 1;
    public InMemoryExpensesRepository() : this(new List<Expense>())
    {
    }

    private InMemoryExpensesRepository(List<Expense> expenses) =>
        _expenses = expenses;

    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> All() => _expenses;
    public IEnumerable<Expense> ByCategory(string category) => 
        _expenses.Where(expense => expense.Category.Name == category);

    public Expense? ById(int id) => _expenses.FirstOrDefault(expense => expense.Id == id);
    public bool Delete(Expense expense) => 
        _expenses.Remove(expense);

    public bool Update(Expense expense)
    {
        var index = _expenses.FindIndex(e => e.Id == expense.Id);
        _expenses[index] = expense;
        return true;
    }
}