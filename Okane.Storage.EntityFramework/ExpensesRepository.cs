using Microsoft.EntityFrameworkCore;
using Okane.Core;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Security;

namespace Okane.Storage.EntityFramework;

public class ExpensesRepository : IExpensesRepository
{
    private readonly OkaneDbContext _db;
    private readonly IUserSession _userSession;
    
    public ExpensesRepository(OkaneDbContext db, IUserSession userSession)
    {
        _db = db;
        _userSession = userSession;
    }

    public void Add(Expense expense)
    {
        _db.Add(expense);
        _db.SaveChanges();
    }

    public IEnumerable<Expense> Retrieve(int page, int pageSize) => 
        UserExpenses().Paginate(page, pageSize);

    public IEnumerable<Expense> ByCategory(string category, int page, int pageSize) => 
        UserExpenses().Where(expense => expense.Category.Name == category).Paginate(page, pageSize);

    public int CountByCategory(string category) => 
        UserExpenses().Count(expense => expense.Category.Name == category);

    public Expense? ById(int id) => 
        UserExpenses()
            .Include(expense => expense.Category)
            .FirstOrDefault(expense => expense.Id == id);

    public bool Delete(Expense expense)
    {
        _db.Remove(expense);
        var recordsAffected = _db.SaveChanges();
        return recordsAffected > 0;
    }

    public bool Update(Expense expense) => 
        _db.SaveChanges() > 0;

    public int Count() => 
        _db.Expenses.Count();

    private IQueryable<Expense> UserExpenses() => 
        _db.Expenses
            .Include(expense => expense.Category)
            .Include(expense => expense.User)
            .Where(expense => expense.User.Id == _userSession.GetCurrentUserId());
}