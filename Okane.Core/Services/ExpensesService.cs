using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Security;
using Okane.Core.Validations;

namespace Okane.Core.Services;

public class ExpensesService : IExpensesService
{
    private const int RecordsPerPage = 10;
    private readonly IExpensesRepository _expenses;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IValidator<CreateExpenseRequest> _validator;
    private readonly Func<DateTime> _getCurrentDate;
    private readonly IUsersRepository _users;
    private readonly IUserSession _userSession;


    public ExpensesService(IExpensesRepository expenses, ICategoriesRepository categoriesRepository,
        IUsersRepository users,
        IValidator<CreateExpenseRequest> validator,
        IUserSession userSession,
        Func<DateTime> getCurrentDate)
    {
        _expenses = expenses;
        _categoriesRepository = categoriesRepository;
        _users = users;
        _validator = validator;
        _userSession = userSession;
        _getCurrentDate = getCurrentDate;
    }

    public (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            return (null, errors);

        var category = _categoriesRepository.ByName(request.CategoryName);

        if (category == null)
            return (null, new CategoryDoesNotExistErrors(request.CategoryName));

        var users = _users.ById(_userSession.GetCurrentUserId());
        
        var expense = new Expense
        {
            Category = category,
            Amount = request.Amount,
            Description = request.Description,
            InvoiceUrl = request.InvoiceUrl,
            User = users,
            CreatedDate = _getCurrentDate()
        };

        _expenses.Add(expense);

        return (ExpenseResponse(expense), null);
    }

    public PaginatedResult<ExpenseResponse> Retrieve(int page = 1, string? category = null)
    {
        var result = category == null
            ? _expenses.Retrieve(page, RecordsPerPage)
            : _expenses.ByCategory(category, page, RecordsPerPage);

        return new PaginatedResult<ExpenseResponse>
        {
            CurrentPage = page,
            RecordsPerPage = RecordsPerPage,
            RecordCount = category == null ? _expenses.Count() : _expenses.CountByCategory(category),
            Records = result.Select(ExpenseResponse)
        };
    }

    public ExpenseResponse? ById(int id)
    {
        var expense = _expenses.ById(id);

        if (expense == null)
            return null;

        return ExpenseResponse(expense);
    }

    public (ExpenseResponse?, Errors?) Update(int id, UpdateExpenseRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Any())
            return (null, errors);
        
        var category = _categoriesRepository.ByName(request.CategoryName);
        if (category == null)
            return (null, new CategoryDoesNotExistErrors(request.CategoryName));
        
        var expenseToUpdate = _expenses.ById(id);

        if (expenseToUpdate == null)
            return (null, new EntityNotFoundErrors(id));
        
        expenseToUpdate.Amount = request.Amount;
        expenseToUpdate.Category = category;
        expenseToUpdate.InvoiceUrl = request.InvoiceUrl;
        expenseToUpdate.Description = request.Description;
        
        _expenses.Update(expenseToUpdate);
        
        return (ExpenseResponse(expenseToUpdate), null);
    }

    public bool Delete(int id)
    {
        var expenseToDelete = _expenses.ById(id);

        return expenseToDelete != null && _expenses.Delete(expenseToDelete);
    }

    private ExpenseResponse ExpenseResponse(Expense expense) =>
        new()
        {
            Id = expense.Id,
            CategoryName = expense.Category.Name,
            Amount = expense.Amount,
            Description = expense.Description,
            InvoiceUrl = expense.InvoiceUrl,
            CreatedDate = expense.CreatedDate,
            User = new UserResponse
            {
                Id = expense.User.Id,
                Email = expense.User.Email
            }
        };
}