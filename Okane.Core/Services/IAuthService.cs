using Okane.Contracts;

namespace Okane.Core.Services;

public interface IAuthService
{
    UserResponse SignUp(SignUpRequest request);
    string GenerateToken(SignInRequest request);
}