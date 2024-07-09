using Core.Configurations;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services.Gateway;
using Infrastructure.Services.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICreditCardRepository, CreditCardRepository>();
        
        services.Configure<RabbitMqConfiguration>(opt => 
            configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(opt));
        
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        
        
        services.Configure<MailerSendAPIConfiguration>(opt => 
            configuration.GetSection(nameof(MailerSendAPIConfiguration)).Bind(opt));
        
        services.AddSingleton<IMailerSendGateway, MailerSendGateway>();
    }
}