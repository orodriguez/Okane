namespace Okane.Contracts;

public class ExpenseResponse
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; }
    public string? InvoiceUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}