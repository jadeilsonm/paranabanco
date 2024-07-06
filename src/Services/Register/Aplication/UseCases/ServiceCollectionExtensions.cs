
using Aplication.UseCases.Create;
using Aplication.UseCases.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication.UseCases;

public static class ServiceCollectionExtensions
{
    public static void AddUseCases(this IServiceCollection services)
    {
        // Customer
        services.AddCustomerUseCases();

        // Producer
        services.AddProducer();
    }
}