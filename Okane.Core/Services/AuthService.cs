using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;
using Okane.Core.Security;

namespace Okane.Core.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _users;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(IPasswordHasher passwordHasher, IUsersRepository users, ITokenGenerator tokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _users = users;
        _tokenGenerator = tokenGenerator;
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

    public string GenerateToken(SignInRequest request)
    {
        var user = _users.ByEmail(request.Email);
        if (!_passwordHasher.Verify(request.Password, user.HashedPassword))
            throw new NotImplementedException();
        return _tokenGenerator.Generate(user);
    }
}