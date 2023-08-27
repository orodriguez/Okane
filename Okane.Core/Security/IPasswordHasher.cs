namespace Okane.Core.Security;

public interface IPasswordHasher
{
    string Hash(string plainPassword);
}