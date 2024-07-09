using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            context.Database.Migrate();
            
            Console.WriteLine("Database migrated successfully.");
            var rabbitMqService = services.GetRequiredService<IRabbitMqService>();
            rabbitMqService.CreateChannel();
            Console.WriteLine("Finish initialize");
        }
        catch (Exception ex)
        {
            // Add scopo de logger
            Console.WriteLine(ex.Message, "An error occurred while migrating the database.");
        }
    }
}