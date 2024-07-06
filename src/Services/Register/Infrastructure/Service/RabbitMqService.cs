using Core.Configurations;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Service;

public class RabbitMqService(IOptionsSnapshot<RabbitMqConfiguration> options) : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration = options.Value;
    private IConnection? _connection;
    
    private const string ExchangeName = "paranabanco-exchange";
    private const string QueueName = "credit-onboarding-queue";
    private const string RoutingKey = "onboarding-customer-key";
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
        
        channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true);
        channel.QueueDeclare(QueueName, true, false, false, null);
        channel.QueueBind(QueueName, ExchangeName, RoutingKey);
    }
}