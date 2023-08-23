namespace Okane.Contracts;

public class CreateExpenseRequest
{
    public int Amount { get; set; } 
    public required string Category { get; set; }
    
    public required string Description { get; set; }
}
