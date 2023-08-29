namespace Okane.Contracts;

public class SignUpRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}