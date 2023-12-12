using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base("one or more validation errors has occured")
    {
        
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToList());
    }

    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}
