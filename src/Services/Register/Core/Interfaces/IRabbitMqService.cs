using RabbitMQ.Client;

namespace Core.Interfaces;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}