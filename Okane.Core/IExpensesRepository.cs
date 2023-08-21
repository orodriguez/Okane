namespace Okane.Tests;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> All();
}