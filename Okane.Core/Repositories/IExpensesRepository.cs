using Okane.Contracts;
using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> All();
    IEnumerable<Expense> ByCategory(string category);
    Expense? ById(int id);
    void Delete (int id);
}

/*
The Include method is used to specify related data that should
be loaded along with the main entity. It's used in queries to include navigation properties,
which are related entities or collections
associated with the primary entity you're querying.
 This can help you retrieve a more complete set of data without making separate queries for related entities.
For example, if you're retrieving expenses by a certain category, 
you might also want to include related data like the user who created the expense.
 By using Include, you can retrieve the related data along with the main entity in a single query, 
 which can improve performance by reducing the number of database queries made.
 */