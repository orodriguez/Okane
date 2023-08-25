namespace Okane.Contracts;

public interface IExpensesService
{
    (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request);
    IEnumerable<ExpenseResponse> Retrieve(string? category);
    ExpenseResponse? ById(int id);
}