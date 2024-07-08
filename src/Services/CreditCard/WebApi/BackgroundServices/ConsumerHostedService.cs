using Core.Interfaces;

namespace WebApi.BackgroundServices;

public class ConsumerHostedService(ICreditCardConsumer creditCardConsumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await creditCardConsumer.ReadeMessageAsync();
    }
}