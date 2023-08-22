using System.ComponentModel.DataAnnotations;
using Okane.Contracts;

namespace Okane.Core.Validations;

public class DataAnnotationsValidator : IValidator<CreateExpenseRequest>
{
    public IDictionary<string, string[]> Validate(CreateExpenseRequest obj)
    {
        var results = new List<ValidationResult>();
        
        var context = new ValidationContext(obj, serviceProvider: null, items: null);

        Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

        return results
            .GroupBy(result => string.Join('+', result.MemberNames))
            .ToDictionary(
                grouping => grouping.Key, 
                ToArray);
    }

    private static string[] ToArray(IGrouping<string, ValidationResult> grouping) => 
        grouping.Select(result => result.ErrorMessage ?? string.Empty).ToArray();
}