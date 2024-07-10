using Core.Configurations;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Services.RabbitMq;

public class RabbitMqService(IOptions<RabbitMqConfiguration> configuration) : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration = configuration.Value;
    private IConnection? _connection;

    private const string ExchangeName = "paranabanco-exchange";
    private List<string> QueueName = new List<string>() { "credit-onboarding-queue", "credit-card-onboarding-queue" };
    
    public IConnection? CreateChannel()
    {
        ConnectionFactory connection = new ConnectionFactory()
        {
            UserName = _configuration.UserName,
            Password = _configuration.Password,
            HostName = _configuration.HostName
        };
        connection.DispatchConsumersAsync = true;
        _connection = connection.CreateConnection();

        CreateExchangeAndQueue();
        return _connection;
    }
    
    private void CreateExchangeAndQueue()
    {
        using IModel? channel = _connection?.CreateModel();
        if (channel == null) 
            return;
        
        channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
        QueueName.ForEach(x =>
        {
            channel.QueueDeclare(x, true, false, false, null);
            channel.QueueBind(x, ExchangeName, string.Empty);
            Console.WriteLine($"Queue {x} initialization");
        });
    }
}