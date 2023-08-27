using Okane.Contracts;
using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> All();
    IEnumerable<Expense> ByCategory(string category);
    Expense? ById(int id);

    bool Delete(int id); // New method for DELETE operation
    Expense? Update(int id, Expense updatedExpense);
}