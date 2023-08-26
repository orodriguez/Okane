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

    public ExpenseResponse Register(CreateExpenseRequest request)
    {
        var category = _categoriesRepository.ByName(request.CategoryName);
        
        var expense = new Expense
        {
            Category = category,
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

    public bool Delete(int id)
    {
        return _expenses.Delete(id);
    }

    public ExpenseResponse? Update(int id, CreateExpenseRequest request)
    {
        var category = _categoriesRepository.ByName(request.CategoryName);
        
        var expense = new Expense
        {
            Category = category,
            Amount = request.Amount,
            Description = request.Description,
            InvoiceUrl = request.InvoiceUrl,
        };

        var updatedExpense = _expenses.Update(id, expense);

        if (updatedExpense != null)
        {
            return CreateResponse(updatedExpense);
        }
        else
        {
            return null;
        }
    }

    private static ExpenseResponse CreateResponse(Expense expense) =>
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