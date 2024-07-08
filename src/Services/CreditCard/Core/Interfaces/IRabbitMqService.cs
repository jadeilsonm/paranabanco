using RabbitMQ.Client;

namespace Core.Interfaces;

public interface IRabbitMqService
{
    public IConnection? CreateChannel();
}