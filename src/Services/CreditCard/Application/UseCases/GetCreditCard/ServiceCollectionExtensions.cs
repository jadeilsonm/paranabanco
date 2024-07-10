using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetCreditCard;

public static class ServiceCollectionExtensions
{
    public static void AddGetCreditCardUseCase(this IServiceCollection services)
    {
        services.AddScoped<IGetCrediCardUseCase, GetCreditCardUseCase>();
    }
}