using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Services;
using Okane.Core.Validations;
using Okane.Storage.InMemory;

namespace Okane.Tests.Services;

public class ExpensesServiceTests
{
    private readonly IExpensesService _expensesService;
    private DateTime _now;

    public ExpensesServiceTests()
    {
        _now = DateTime.Parse("2023-01-01");

        var categories = new InMemoryCategoriesRepository();
        categories.Add(new Category { Name = "Food" });
        categories.Add(new Category { Name = "Entertainment" });
        categories.Add(new Category { Name = "Groceries" });

        _expensesService = new ExpensesService(
            new InMemoryExpensesRepository(),
            categories,
            new DataAnnotationsValidator<CreateExpenseRequest>(),
            () => _now);
    }

    [Fact]
    public void RegisterExpense()
    {
        _now = DateTime.Parse("2023-08-23");

        var (expense, _) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Groceries",
            Amount = 10,
            Description = "Food for dinner",
            InvoiceUrl = "http://invoices.com/1"
        });

        Assert.NotNull(expense);
        Assert.Equal(1, expense.Id);
        Assert.Equal(10, expense.Amount);
        Assert.Equal("Groceries", expense.CategoryName);
        Assert.Equal("Food for dinner", expense.Description);
        Assert.Equal("http://invoices.com/1", expense.InvoiceUrl);
        Assert.Equal(DateTime.Parse("2023-08-23"), expense.CreatedDate);
    }

    [Fact]
    public void RegisterExpense_AmountZero()
    {
        var (_, errors) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Food",
            Amount = 0
        });

        Assert.NotNull(errors);
        var (property, propertyErrors) = Assert.Single(errors);
        Assert.Equal(nameof(CreateExpenseRequest.Amount), property);
        var error = Assert.Single(propertyErrors);
        Assert.Equal("The field Amount must be between 1 and 1000000.", error);
    }

    [Fact]
    public void RegisterExpense_WithNonExistingCategory()
    {
        var (_, errors) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Weird Category",
            Amount = 10
        });

        Assert.NotNull(errors);
        var (property, propertyErrors) = Assert.Single(errors);
        Assert.Equal(nameof(CreateExpenseRequest.CategoryName), property);
        var error = Assert.Single(propertyErrors);
        Assert.Equal("Category with Name Weird Category does not exist", error);
    }

    [Fact]
    public void RetrieveAllExpenses()
    {
        _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Groceries",
            Amount = 10
        });

        _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Entertainment",
            Amount = 20
        });

        var allExpenses = _expensesService.Retrieve();

        Assert.Equal(2, allExpenses.Count());
    }

    [Fact]
    public void RetrieveAllExpenses_FilterByCategory()
    {
        _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Groceries",
            Amount = 10
        });

        _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Entertainment",
            Amount = 20
        });

        var expenses = _expensesService.Retrieve("Groceries");

        var expense = Assert.Single(expenses);
        Assert.Equal("Groceries", expense.CategoryName);
    }

    [Fact]
    public void GetById()
    {
        var (createdExpense, _) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Groceries",
            Amount = 10
        });

        Assert.NotNull(createdExpense);
        var retrievedExpense = _expensesService.ById(createdExpense.Id);

        Assert.NotNull(retrievedExpense);
        Assert.Equal(createdExpense.CategoryName, retrievedExpense.CategoryName);
    }

    [Fact]
    public void Delete()
    {
        var (createdExpense, _) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Food",
            Amount = 20
        });
        
        Assert.NotNull(createdExpense);

        var isSuccess = _expensesService.Delete(createdExpense.Id);
        Assert.True(isSuccess);
        
        var nonExistingExpense = _expensesService.ById(createdExpense.Id);
        Assert.Null(nonExistingExpense);

    }
    
    [Fact]
    public void Delete_ExpenseDoesNotExist()
    {
        const int unknownExpenseId = 12345;
        var isSuccess = _expensesService.Delete(unknownExpenseId);
        Assert.False(isSuccess);
    }
}