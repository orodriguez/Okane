using System.Collections;

namespace Okane.Contracts;

public class CategoryResponse
{

    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required IEnumerable?  Expenses { get; set; }
}