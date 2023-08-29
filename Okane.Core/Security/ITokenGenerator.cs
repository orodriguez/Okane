using Okane.Core.Entities;

namespace Okane.Core.Security;

public interface ITokenGenerator
{
    string Generate(User user);
}