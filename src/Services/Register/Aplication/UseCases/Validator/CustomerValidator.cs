using Aplication.DTOs;
using FluentValidation;

namespace Aplication.UseCases.Validator;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address");
        RuleFor(x => x.CellNumber)
            .NotNull()
            .WithMessage("Invalid email address");
        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password is required");
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required");
        RuleFor(x => x.Document)
            .NotEmpty()
            .WithMessage("Document is required");
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");
        
    }
}