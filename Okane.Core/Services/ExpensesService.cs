using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

public class ExpensesService : IExpensesService
{
    private readonly IExpensesRepository _expenses;

    public ExpensesService(IExpensesRepository expenses) =>
        _expenses = expenses;

    public ExpenseResponse Register(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Category = request.Category,
            Amount = request.Amount,
            Description = request.Description
        };
        
        _expenses.Add(expense);
        
        return new ExpenseResponse
        {
            Id = expense.Id,
            Category = expense.Category,
            Amount = expense.Amount,
            Description = expense.Description
        };
    }

    public IEnumerable<ExpenseResponse> RetrieveAll() => 
        _expenses.All().Select(expense => new ExpenseResponse
        {
            Amount = expense.Amount,
            Category = expense.Category,
            Description = expense.Description
        });
}
