using Core.Interfaces;

namespace WebApi.BackgroudService;

public class ConsumerHostedService(IOnboardingConsumer onboardingConsumer) : BackgroundService
{
    private readonly IOnboardingConsumer onboardingConsumer = onboardingConsumer;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await onboardingConsumer.ReadeMessageAsync();
    }
}