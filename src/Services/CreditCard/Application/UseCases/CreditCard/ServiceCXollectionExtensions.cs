using Application.DTOs;
using Application.UseCases.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.CreditCard;

public static class ServiceCXollectionExtensions
{
    public static void AddCreditCardUseCase(this IServiceCollection services)
    {
        // validato
        services.AddTransient<IValidator<CreditCardEvent>, CreditCardValidator>();
        
        // usecase
        services.AddTransient<ICreditCardUseCase, CreditCardUseCase>();
    }
}