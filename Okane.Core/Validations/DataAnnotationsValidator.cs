using System.ComponentModel.DataAnnotations;
using Okane.Contracts;

namespace Okane.Core.Validations;

public class DataAnnotationsValidator<T> : IValidator<T>
{
    public Errors Validate(T obj)
    {
        var results = new List<ValidationResult>();
        
        var context = new ValidationContext(obj, serviceProvider: null, items: null);

        Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

        var errorsDictionary = results
            .GroupBy(result => string.Join('+', result.MemberNames))
            .ToDictionary(
                grouping => grouping.Key, 
                ToArray);
        return new Errors(errorsDictionary);
    }

    private static string[] ToArray(IGrouping<string, ValidationResult> grouping) => 
        grouping.Select(result => result.ErrorMessage ?? string.Empty).ToArray();
}