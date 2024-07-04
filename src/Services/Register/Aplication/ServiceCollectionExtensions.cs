using Aplication.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Add UseCases
        services.AddUseCases();
    }
}