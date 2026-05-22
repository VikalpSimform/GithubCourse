using System.ComponentModel.DataAnnotations;

namespace GithubCourse.Services;

public interface IValidationService
{
    (bool IsValid, IEnumerable<string> Errors) Validate<T>(T model) where T : class;
}

public class ValidationService : IValidationService
{
    public (bool IsValid, IEnumerable<string> Errors) Validate<T>(T model) where T : class
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        
        return (isValid, results.Select(r => r.ErrorMessage ?? "Unknown validation error"));
    }
}
