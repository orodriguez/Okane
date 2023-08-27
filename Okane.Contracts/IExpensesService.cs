namespace Okane.Contracts;

public interface IExpensesService
{
    (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request);
    IEnumerable<ExpenseResponse> Retrieve(string? category = null);
    ExpenseResponse? ById(int id);
    (ExpenseResponse?, Errors?) Update(int id, UpdateExpenseRequest request);
    bool Delete(int id);
}