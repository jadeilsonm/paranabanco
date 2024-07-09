using Application.DTOs;
using Application.UseCases.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.CreditCard;

public static class ServiceCollectionExtensions
{
    public static void AddCreditCardUseCase(this IServiceCollection services)
    {
        // validato
        services.AddSingleton<IValidator<CreditCardEvent>, CreditCardValidator>();
        
        // usecase
        services.AddSingleton<ICreditCardUseCase, CreditCardUseCase>();
    }
}