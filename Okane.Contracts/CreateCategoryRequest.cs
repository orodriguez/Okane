using System.ComponentModel.DataAnnotations;

namespace Okane.Contracts;

public class CreateCategoryRequest
{
    [Required]
    [MaxLength(80)]
    public required string Name { get; set; }
}