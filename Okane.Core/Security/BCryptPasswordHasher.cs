

namespace Okane.Core.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string plainPassword) => 
        BCrypt.Net.BCrypt.HashPassword(plainPassword);

    public bool Verify(string plainPassword, string hashedPassword) => 
        BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
}