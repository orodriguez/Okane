using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryUsersRepository : IUsersRepository
{
    private int _nextId = 1;
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
    }

    public User? ByEmail(string email) => 
        _users.FirstOrDefault(user => user.Email == email);

    public User ById(int id) => 
        _users.First(user => user.Id == id);
}