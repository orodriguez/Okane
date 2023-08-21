using Okane.Core.Entities;

namespace Okane.Core.Services;

public interface IExpensesService
{
    Expense Register(Expense expense);
    IEnumerable<Expense> RetrieveAll();
}