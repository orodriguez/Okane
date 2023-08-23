using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

public class ExpensesService : IExpensesService
{
    private readonly IExpensesRepository _expenses;
    private readonly Func<DateTime> _getCurrentDate;

    public ExpensesService(IExpensesRepository expenses, Func<DateTime> getCurrentDate)
    {
        _expenses = expenses;
        _getCurrentDate = getCurrentDate;
    }

    public ExpenseResponse Register(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Category = request.Category,
            Amount = request.Amount,
            Description = request.Description,
            InvoiceUrl = request.InvoiceUrl,
            CreatedDate = _getCurrentDate()
        };

        _expenses.Add(expense);

        return CreateResponse(expense);
    }

    public IEnumerable<ExpenseResponse> Retrieve(string? category = null)
    {
        var result = category == null ? _expenses.All() : _expenses.ByCategory(category);

        return result
            .Select(CreateResponse);
    }

    public ExpenseResponse? ById(int id)
    {
        var expense = _expenses.ById(id);

        if (expense == null)
            return null;

        return CreateResponse(expense);
    }

    private static ExpenseResponse CreateResponse(Expense expense) =>
        new()
        {
            Id = expense.Id,
            Category = expense.Category,
            Amount = expense.Amount,
            Description = expense.Description,
            InvoiceUrl = expense.InvoiceUrl,
            CreatedDate = expense.CreatedDate
        };
}