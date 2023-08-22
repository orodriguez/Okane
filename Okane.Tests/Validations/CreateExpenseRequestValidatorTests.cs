using Okane.Contracts;
using Okane.Core.Validations;

namespace Okane.Tests.Validations;

public class CreateExpenseRequestValidatorTests
{
    private readonly IValidator<CreateExpenseRequest> _validator;

    public CreateExpenseRequestValidatorTests() => 
        _validator = new DataAnnotationsValidator();

    [Fact]
    public void Valid()
    {
        var result = _validator.Validate(new CreateExpenseRequest
        {
            Amount = 10,
            Category = "Food"
        });
        
        Assert.Empty(result);
    }
    
    [Fact]
    public void CategoryTooLong()
    {
        var result = _validator.Validate(new CreateExpenseRequest
        {
            Amount = 10,
            Category = string.Concat(Enumerable.Repeat("x", 100))
        });
        
        var (property, errors) = Assert.Single(result);
        Assert.Equal("Category", property);

        var error = Assert.Single(errors);
        Assert.Equal("The field Category must be a string or array type with a maximum length of '80'.", error);
    }
}