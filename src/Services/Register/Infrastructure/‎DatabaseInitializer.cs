using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class DatabaseInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            // Add scopo de logger
            Console.WriteLine(ex.Message, "An error occurred while migrating the database.");
        }
    }
}