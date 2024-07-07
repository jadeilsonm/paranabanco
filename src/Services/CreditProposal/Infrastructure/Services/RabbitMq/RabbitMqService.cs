using Core.Configurations;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Services.RabbitMq;

public class RabbitMqService(IOptions<RabbitMqConfiguration> options) : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration = options.Value;
    private IConnection? _connection;
    
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

        return _connection;
    }
}