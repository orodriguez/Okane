namespace Okane.Contracts;

public interface IExpensesService
{
    (ExpenseResponse?, Errors?) Register(CreateExpenseRequest request);
    PaginatedResult<ExpenseResponse> Retrieve(int page = 1, string? category = null);
    ExpenseResponse? ById(int id);
    (ExpenseResponse?, Errors?) Update(int id, UpdateExpenseRequest request);
    bool Delete(int id);
}