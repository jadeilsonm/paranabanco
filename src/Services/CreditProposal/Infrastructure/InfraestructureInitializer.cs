﻿using Core.Interfaces;
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
            var rabbitMqService = services.GetRequiredService<IRabbitMqService>();
            rabbitMqService.CreateChannel();
        }
        catch (Exception ex)
        {
            // Add scopo de logger
            Console.WriteLine(ex.Message, "An error occurred while migrating the database.");
        }
    }
}