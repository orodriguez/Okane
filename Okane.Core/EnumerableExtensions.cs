using Okane.Core.Entities;

namespace Okane.Core;

public static class EnumerableExtensions
{
    public static IEnumerable<Expense> Paginate(this IQueryable<Expense> source, int page, int pageSize) =>
        source
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
}