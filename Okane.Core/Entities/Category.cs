using System.Collections.ObjectModel;

namespace Okane.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Expense> Expenses { get; set; } = new Collection<Expense>();
}