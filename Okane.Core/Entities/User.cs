namespace Okane.Core.Entities;

public class User
{
    public int Id { get; set; }
    public required string HashedPassword { get; set; }
    public required string Email { get; set; }
}