using System.Net;
using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

internal class ExpensesService : IExpensesService
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
            Description = expense.Description
        };
    public void Delete(int id)
    {
        var expenseToDelete = _expenses.ById(id);

        if (expenseToDelete == null)
        {
            
            throw new NotFoundException("Expense not found.");
        }

        _expenses.Delete(id);
    }

    public void Update(ExpenseResponse id)
    {
        throw new NotImplementedException();
    }
    
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        throw new NotImplementedException();
    }
}

public class UpdateExpenseRequest
{
    public int Amount { get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; }
    
    public string? InvoiceUrl { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
}
