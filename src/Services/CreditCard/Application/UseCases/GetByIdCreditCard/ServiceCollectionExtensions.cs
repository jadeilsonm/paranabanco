using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetByIdCreditCard;

public static class ServiceCollectionExtensions
{
    public static void AddGetByIdCreditCardUseCase(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdCreditCardUseCase, GetByIdCreditCardUseCase>();
    }
}