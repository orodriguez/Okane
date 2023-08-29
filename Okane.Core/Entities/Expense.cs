namespace Okane.Core.Entities;

public class Expense
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string? Description { get; set; }
    public string? InvoiceUrl { get; set; }
    public DateTime CreatedDate { get; set; }

    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public required User User { get; set; }
}
