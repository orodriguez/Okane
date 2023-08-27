using Moq;
using Okane.Contracts;
using Okane.Core.Security;
using Okane.Core.Services;
using Okane.Storage.InMemory;

namespace Okane.Tests.Services;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly InMemoryUsersRepository _usersRepository;

    public AuthServiceTests()
    {
        _usersRepository = new InMemoryUsersRepository();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _authService = new AuthService(_mockPasswordHasher.Object, _usersRepository);
    }

    [Fact]
    public void SignUp()
    {
        const string password = "1234";
        const string hashedPassword = "HASHED_PASSWORD";
        
        _mockPasswordHasher
            .Setup(hasher => hasher.Hash(password))
            .Returns(hashedPassword);

        var createdUser = _authService.SignUp(new SignUpRequest
        {
            Email = "john@mail.com",
            Password = password
        });
        
        Assert.Equal(1, createdUser.Id);
        Assert.Equal("john@mail.com", createdUser.Email);

        var userEntity = _usersRepository.ById(createdUser.Id);
        Assert.Equal(hashedPassword, userEntity.HashedPassword);
    }
}