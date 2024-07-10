using Aplication.UseCases.Producer;
using Core.Configurations;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Service;
using Infrastructure.Service.Gateway;
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
        
        services.Configure<MailerSendAPIConfiguration>(opt => 
            configuration.GetSection(nameof(MailerSendAPIConfiguration)).Bind(opt));
        
        services.AddSingleton<IMailerSendGateway, MailerSendGateway>();
    }
}