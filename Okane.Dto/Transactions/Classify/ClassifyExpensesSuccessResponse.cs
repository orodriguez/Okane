namespace Okane.Dto.Transactions.Classify
{
  public abstract class ClassifyExpensesSuccessResponse : ClassifyExpensesResponse
  {
    public IEnumerable<Transaction> Records { get; set; }
  }
}