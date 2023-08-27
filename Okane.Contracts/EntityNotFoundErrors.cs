namespace Okane.Contracts;

public class EntityNotFoundErrors : Errors
{
    public EntityNotFoundErrors(int id) : base("Id", $"Entity with Id {id} not found")
    {
    }
}