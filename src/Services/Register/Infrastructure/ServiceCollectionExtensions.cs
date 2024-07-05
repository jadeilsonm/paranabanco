using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}