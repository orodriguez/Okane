using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Security;

namespace Okane.Core.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _users;

    public AuthService(IPasswordHasher passwordHasher, IUsersRepository users)
    {
        _passwordHasher = passwordHasher;
        _users = users;
    }

    public UserResponse SignUp(SignUpRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            HashedPassword = _passwordHasher.Hash(request.Password)
        };

        _users.Add(user);

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}