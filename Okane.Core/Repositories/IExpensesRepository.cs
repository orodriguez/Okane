using Okane.Contracts;
using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> Retrieve(int page, int pageSize);
    Expense? ById(int id);
    bool Delete(Expense expense);
    bool Update(Expense expense);
    int Count();
    IEnumerable<Expense> ByCategory(string category, int page, int pageSize);
    int CountByCategory(string category);
}