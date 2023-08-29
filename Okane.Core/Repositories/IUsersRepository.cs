using Okane.Contracts;
using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IUsersRepository
{
    void Add(User user);
    User? ByEmail(string requestEmail);
    User ById(int id);
}