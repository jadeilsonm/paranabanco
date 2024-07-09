using System.Text;
using System.Text.Json;
using Aplication.DTOs;
using Core.Interfaces;
using RabbitMQ.Client;

namespace Aplication.UseCases.Producer;

public class ProducerOnboard(IRabbitMqService rabbitMqService) : IProducerOnboard
{
    private readonly IRabbitMqService _rabbitMqService = rabbitMqService ?? throw new ArgumentNullException(nameof(rabbitMqService));
    
    private const string ExchangeName = "paranabanco-exchange";

    public void Send(CustomerEvent message, CancellationToken cancellationToken = default)
    {
        using var connection = _rabbitMqService.CreateChannel();
        using var model = connection?.CreateModel();
        
        string json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        IBasicProperties? basicProperties = model?.CreateBasicProperties();
        basicProperties!.CorrelationId = message.Id.ToString();
        
        model.BasicPublish(ExchangeName,
            string.Empty,
            basicProperties: basicProperties,
            body: body);
        
    }
}