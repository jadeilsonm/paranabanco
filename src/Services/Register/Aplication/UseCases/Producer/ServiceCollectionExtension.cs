using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication.UseCases.Producer;

public static class ServiceCollectionExtension
{
    public static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IProducerOnboard, ProducerOnboard>();
    }
}