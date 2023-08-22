namespace Okane.Contracts;

public interface IExpensesService
{
    ExpenseResponse Register(CreateExpenseRequest request);
    IEnumerable<ExpenseResponse> Retrieve(string? category);
    ExpenseResponse? ById(int id);
}