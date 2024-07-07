﻿using Application.DTOs;
using FluentValidation;

namespace Application.UseCases.Validator;

public class OnboardingValidate : AbstractValidator<OnboardingCustomeEvent>
{
    public OnboardingValidate()
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
        /*RuleFor(x => x.Salary)
            .NotEmpty()
            .WithMessage("Salary is required");
        RuleFor(x => x.AmountAll)
            .NotEmpty()
            .WithMessage("AmountAll is required");*/
    }
}