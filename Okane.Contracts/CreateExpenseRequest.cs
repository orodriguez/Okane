using System.ComponentModel.DataAnnotations;

namespace Okane.Contracts;

public class CreateExpenseRequest
{
    [Required]
    [Range(1, 1_000_000)]
    public int Amount { get; set; }
    
    [Required]
    [MaxLength(80)]
    public required string Category { get; set; }

    public string? Description { get; set; }

    [Url(ErrorMessage = "The InvoiceUrl field must be a valid URL.")]
    public string? InvoiceUrl { get; set; }

    public DateTime CreatedDate { get; set; }
}