using Microsoft.EntityFrameworkCore;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class ExpensesRepository : IExpensesRepository
{
    private readonly OkaneDbContext _db;

    public ExpensesRepository(OkaneDbContext db) => _db = db;

    public void Add(Expense expense)
    {
        _db.Expenses.Add(expense);
        _db.SaveChanges();
    }

    public IEnumerable<Expense> All() => 
        _db.Expenses;

    public IEnumerable<Expense> ByCategory(string category) => 
        _db.Expenses.Where(expense => expense.Category.Name == category)
            .Include(expense => expense.Category);

    public Expense? ById(int id) => 
        _db.Expenses.FirstOrDefault(expense => expense.Id == id);

    public bool Delete(int id)
    {
        Expense? entity = ById(id);

        if (entity != null)
        {
            _db.Expenses.Remove(entity);
            _db.SaveChanges();
            return true;
        }

        return false;
    }

    public Expense? Update(int id, Expense expense)
    {
        var oldExpense = ById(id);

        if (oldExpense == null)
        {
            return null;
        }
        
        oldExpense.Amount = expense.Amount == null ? oldExpense.Amount : expense.Amount;
        oldExpense.Category = expense.Category == null ? oldExpense.Category : expense.Category;
        oldExpense.Description = expense.Description == null ? oldExpense.Description : expense.Description;
        oldExpense.InvoiceUrl = expense.InvoiceUrl == null ? oldExpense.InvoiceUrl : expense.InvoiceUrl;
        
        _db.SaveChanges();
        return oldExpense;
    }
}