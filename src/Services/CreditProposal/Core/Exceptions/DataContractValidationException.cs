using FluentValidation.Results;

namespace Core.Exceptions;

public class DataContractValidationException(string message, List<ValidationFailure> validationErrors) : Exception(message)
{
    public List<ValidationFailure> ValidationErrors { get; init; }
}