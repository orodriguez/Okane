namespace Okane.Contracts;

public class SignInRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}