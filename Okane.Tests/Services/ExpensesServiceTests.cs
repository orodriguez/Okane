using Microsoft.Extensions.Options;
using Moq;
using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Security;
using Okane.Core.Services;
using Okane.Core.Validations;
using Okane.Storage.InMemory;

namespace Okane.Tests.Services;

public class ExpensesServiceTests
{
    private const string Password = "1234";
    private readonly IExpensesService _expensesService;
    private readonly InMemoryUserSession _session;
    private DateTime _now;
    private readonly IAuthService _authService;
    private readonly UserResponse _currentUser;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;

    public ExpensesServiceTests()
    {
        _session = new InMemoryUserSession();
        _now = DateTime.Parse("2023-01-01");

        var usersRepository = new InMemoryUsersRepository();
        
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockPasswordHasher.Setup(hasher => hasher.Hash(Password))
            .Returns(Password);
        _mockPasswordHasher.Setup(hasher => hasher.Verify(Password, It.IsAny<string>()))
            .Returns(true);
        
        _authService = new AuthService(
            _mockPasswordHasher.Object,
            usersRepository,
            new JwtTokenGenerator(Options.Create(new JwtSettings
            {
                Audience = "public",
                Issuer = "http://issuer.com",
                SecretKey = "Super secret and long key, it must be long",
                ExpirationMinutes = 60
            })));
        
        _currentUser = _authService.SignUp(new SignUpRequest
        {
            Email = "test@mail.com",
            Password = Password
        });
        
        // Simulates SignIn
        _session.SetCurrentUserId(_currentUser.Id);
        
        var categories = new InMemoryCategoriesRepository();
        categories.Add(new Category { Name = "Food" });
        categories.Add(new Category { Name = "Entertainment" });
        categories.Add(new Category { Name = "Groceries" });
        
        _expensesService = new ExpensesService(
            new InMemoryExpensesRepository(),
            categories,
            usersRepository,
            new DataAnnotationsValidator<CreateExpenseRequest>(),
            _session,
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
        Assert.Equal(_currentUser.Id, expense.User.Id);
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
    public void Update()
    {
        var (createdExpense, _) = _expensesService.Register(new CreateExpenseRequest
        {
            CategoryName = "Food",
            Amount = 50
        });
        
        Assert.NotNull(createdExpense);
        
        var (updatedExpense, _) = _expensesService.Update(createdExpense.Id, new UpdateExpenseRequest
        {
            CategoryName = "Groceries",
            Amount = 40,
            InvoiceUrl = "http://invoices.com/1",
            Description = "Updated Description"
        });
        
        Assert.NotNull(updatedExpense);
        Assert.Equal(createdExpense.Id, updatedExpense.Id);
        Assert.Equal(40, updatedExpense.Amount);
        Assert.Equal("Groceries", updatedExpense.CategoryName);
        Assert.Equal("Updated Description", updatedExpense.Description);
        Assert.Equal("http://invoices.com/1", updatedExpense.InvoiceUrl);
    }
    
    [Fact]
    public void Update_ValidationErrors()
    {
        var (_, errors) = _expensesService.Update(1, new UpdateExpenseRequest
        {
            CategoryName = "",
            Amount = 50
        });
        
        Assert.NotNull(errors);
        var (property, propertyErrors) = Assert.Single(errors);
        Assert.Equal(nameof(UpdateExpenseRequest.CategoryName), property);
        var error = Assert.Single(propertyErrors);
        Assert.Equal("The CategoryName field is required.", error);
    }
    
    [Fact]
    public void Update_ExpenseIdNotFound()
    {
        const int unknownId = 555;
        var (_, errors) = _expensesService.Update(unknownId, new UpdateExpenseRequest
        {
            CategoryName = "Food",
            Amount = 50
        });
        
        Assert.NotNull(errors);
        var (property, propertyErrors) = Assert.Single(errors);
        Assert.Equal("Id", property);
        var error = Assert.Single(propertyErrors);
        Assert.Equal("Entity with Id 555 not found", error);
    }
    
    [Fact]
    public void Update_CategoryNotFound()
    {
        var (_, errors) = _expensesService.Update(1, new UpdateExpenseRequest
        {
            CategoryName = "Unknown",
            Amount = 50
        });
        
        Assert.NotNull(errors);
        var (property, propertyErrors) = Assert.Single(errors);
        Assert.Equal(nameof(UpdateExpenseRequest.CategoryName), property);
        var error = Assert.Single(propertyErrors);
        Assert.Equal("Category with Name Unknown does not exist", error);
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