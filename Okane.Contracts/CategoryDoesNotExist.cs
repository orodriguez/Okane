namespace Okane.Contracts;

public class CategoryDoesNotExist : Errors
{
    public CategoryDoesNotExist(string categoryName) : base(
        nameof(CreateExpenseRequest.CategoryName),
        $"Category with Name {categoryName} does not exist")
    {
    }
}