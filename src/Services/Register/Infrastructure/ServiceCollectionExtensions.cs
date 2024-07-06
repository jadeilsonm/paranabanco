using Aplication.UseCases.Producer;
using Core.Configurations;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        services.Configure<RabbitMqConfiguration>(opt => 
            configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(opt));
        
        services.AddScoped<IRabbitMqService, RabbitMqService>();
    }
}