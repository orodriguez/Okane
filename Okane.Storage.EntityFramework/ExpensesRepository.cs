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
        _db.Expenses.Include(expense => expense.Category);

    public IEnumerable<Expense> ByCategory(string category) => 
        _db.Expenses.Where(expense => expense.Category.Name == category)
            .Include(expense => expense.Category);

    public Expense? ById(int id) => 
        _db.Expenses
            .Include(expense => expense.Category)
            .FirstOrDefault(expense => expense.Id == id);

    public bool Delete(Expense expense)
    {
        _db.Expenses.Remove(expense);
        var recordsAffected = _db.SaveChanges();
        return recordsAffected > 0;
    }

    public bool Update(Expense expense) => 
        _db.SaveChanges() > 0;
}