namespace Okane.Contracts;

public class CategoryDoesNotExistErrors : Errors
{
    public CategoryDoesNotExistErrors(string categoryName) : base(
        nameof(CreateExpenseRequest.CategoryName),
        $"Category with Name {categoryName} does not exist")
    {
    }
}