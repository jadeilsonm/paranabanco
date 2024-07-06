using System.Text;
using Aplication.DTOs;
using Core.Interfaces;
using RabbitMQ.Client;

namespace Aplication.UseCases.Producer;

public class ProducerOnboard(IRabbitMqService rabbitMqService) : IProducerOnboard
{
    private readonly IRabbitMqService _rabbitMqService = rabbitMqService ?? throw new ArgumentNullException(nameof(rabbitMqService));

    public void Send(CustomerResponse message, CancellationToken cancellationToken = default)
    {
        using var connection = _rabbitMqService.CreateChannel();
        using var model = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(message.ToString());
        model.BasicPublish("UserExchange",
            string.Empty,
            basicProperties: null,
            body: body);
    }
}