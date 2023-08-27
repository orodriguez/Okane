using Okane.Contracts;

namespace Okane.Core.Validations;

public interface IValidator<in T>
{
    Errors Validate(T obj);
}