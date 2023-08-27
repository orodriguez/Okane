using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class UsersRepository : IUsersRepository
{
    private readonly OkaneDbContext _db;

    public UsersRepository(OkaneDbContext db) => _db = db;

    public void Add(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }
}