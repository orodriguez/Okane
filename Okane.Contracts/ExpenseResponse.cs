namespace Okane.Contracts;

public class ExpenseResponse
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public required string Category { get; set; }
    public required string Description { get; set; }
}
