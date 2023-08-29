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

    public User? ByEmail(string email) => 
        _db.Users.FirstOrDefault(user => user.Email == email);

    public User? ByCredentials(string email, string hashedPassword) => 
        _db.Users.FirstOrDefault(user => user.Email == email && user.HashedPassword == hashedPassword);
}