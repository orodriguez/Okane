using System.IdentityModel.Tokens.Jwt;
using Moq;
using Okane.Contracts;
using Okane.Core.Security;
using Okane.Core.Services;
using Okane.Storage.InMemory;

namespace Okane.Tests.Services;

public class AuthServiceTests
{
    const string Password = "1234";
    const string HashedPassword = "HASHED_PASSWORD";
    private readonly IAuthService _authService;
    private readonly InMemoryUsersRepository _usersRepository;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;

    public AuthServiceTests()
    {
        _usersRepository = new InMemoryUsersRepository();
        
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockPasswordHasher.Setup(hasher => hasher.Hash(Password))
            .Returns(Password);
        _mockPasswordHasher.Setup(hasher => hasher.Verify(Password, It.IsAny<string>()))
            .Returns(true);
        
        _authService = new AuthService(
            _mockPasswordHasher.Object,
            _usersRepository,
            new JwtTokenGenerator());
    }

    [Fact]
    public void SignUp()
    {
        var createdUser = _authService.SignUp(new SignUpRequest
        {
            Email = "john@mail.com",
            Password = Password
        });

        Assert.Equal(1, createdUser.Id);
        Assert.Equal("john@mail.com", createdUser.Email);

        var userEntity = _usersRepository.ById(createdUser.Id);
        Assert.NotNull(userEntity.HashedPassword);
        
        _mockPasswordHasher.Verify(hasher => hasher.Hash(Password), Times.Once);
    }

    [Fact]
    public void GenerateToken()
    {
        var createdUser = _authService.SignUp(new SignUpRequest
        {
            Email = "john@mail.com",
            Password = Password
        });

        var token = _authService.GenerateToken(new SignInRequest
        {
            Email = "john@mail.com",
            Password = Password
        });

        Assert.NotNull(token);

        var tokenObj = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var emailClaim = tokenObj.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email);
        Assert.NotNull(emailClaim);
        Assert.Equal("john@mail.com", emailClaim.Value);
    }
}