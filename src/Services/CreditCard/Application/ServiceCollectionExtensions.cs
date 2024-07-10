using Application.UseCases.CreditCard;
using Application.UseCases.GetByIdCreditCard;
using Application.UseCases.GetCreditCard;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddCreditCardUseCase();
        services.AddGetCreditCardUseCase();
        services.AddGetByIdCreditCardUseCase();
    }
}