using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

public class ExpensesService : IExpensesService
{
    private readonly IExpensesRepository _expenses;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly Func<DateTime> _getCurrentDate;

    public ExpensesService(IExpensesRepository expenses, ICategoriesRepository categoriesRepository,
        Func<DateTime> getCurrentDate)
    {
        _expenses = expenses;
        _categoriesRepository = categoriesRepository;
        _getCurrentDate = getCurrentDate;
    }

    public (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request)
    {
        var category = _categoriesRepository.ByName(request.CategoryName);

        if (category == null)
            return (null, new CategoryDoesNotExist(request.CategoryName));

        var expense = new Expense
        {
            Category = category,
            Amount = request.Amount,
            Description = request.Description,
            InvoiceUrl = request.InvoiceUrl,
            CreatedDate = _getCurrentDate()
        };

        _expenses.Add(expense);

        return (ExpenseResponse(expense), null);
    }

    public IEnumerable<ExpenseResponse> Retrieve(string? category = null)
    {
        var result = category == null ? _expenses.All() : _expenses.ByCategory(category);

        return result
            .Select(ExpenseResponse);
    }

    public ExpenseResponse? ById(int id)
    {
        var expense = _expenses.ById(id);

        if (expense == null)
            return null;

        return ExpenseResponse(expense);
    }

    private static ExpenseResponse ExpenseResponse(Expense expense) =>
        new()
        {
            Id = expense.Id,
            CategoryName = expense.Category.Name,
            Amount = expense.Amount,
            Description = expense.Description,
            InvoiceUrl = expense.InvoiceUrl,
            CreatedDate = expense.CreatedDate
        };
}