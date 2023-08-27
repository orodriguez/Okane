namespace Okane.Contracts;

public interface IExpensesService
{
    (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request);
    IEnumerable<ExpenseResponse> Retrieve(string? category = null);
    ExpenseResponse? ById(int id);
    bool Delete(int id);
}