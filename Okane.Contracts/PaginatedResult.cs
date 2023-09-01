namespace Okane.Contracts;

public class PaginatedResult<T>
{
    public int RecordCount { get; set; }
    public int RecordsPerPage { get; set; }
    
    // public int PagesCount => RecordCount / RecordsPerPage + (RecordCount % RecordsPerPage > 0 ? 1 : 0);
    public int PagesCount => (int)Math.Ceiling((double)RecordCount / RecordsPerPage);
    public int CurrentPage { get; set; }
    public required IEnumerable<T> Records { get; set; }
}