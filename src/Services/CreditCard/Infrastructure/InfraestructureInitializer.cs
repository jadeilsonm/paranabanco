using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class InfraestructureInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
            
            var rabbitMqService = services.GetRequiredService<IRabbitMqService>();
            rabbitMqService.CreateChannel();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<InfraestructureInitializer>>();
            logger.LogError(ex, "An error occurred while initializing the infrastructure.");
        }
    }
}