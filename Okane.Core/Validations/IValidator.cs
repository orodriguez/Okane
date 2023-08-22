namespace Okane.Core.Validations;

public interface IValidator<in T>
{
    IDictionary<string, string[]> Validate(T obj);
}