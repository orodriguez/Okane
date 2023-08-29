using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IUsersRepository
{
    void Add(User user);
    User? ByEmail(string requestEmail);
}