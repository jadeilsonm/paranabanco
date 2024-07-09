using System.Text.Json;
using Application.DTOs;
using Application.UseCases.CreditCard;
using Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApi.Consumer;

public class CreditConsumer : ICreditCardConsumer, IDisposable
{
    private readonly IModel _model;
    private readonly IConnection _connection;
    private readonly ICreditCardUseCase _useCase;
    private readonly ILogger<CreditConsumer> _logger;
    
    private const string QueueName = "credit-card-onboarding-queue";
    
    public CreditConsumer(IRabbitMqService rabbitMqService, ICreditCardUseCase useCase, ILogger<CreditConsumer> logger)
    {
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();
        _useCase = useCase;
        _logger = logger;
    }
    
    public Task ReadeMessageAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);
        
        var maxRetryAttempts = 3;
        var retryDelay = TimeSpan.FromSeconds(5);
        
        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = JsonSerializer.Deserialize<CreditCardEvent>(body);
            
            for (int attempt = 0; attempt < maxRetryAttempts; attempt++)
            {
                try
                {
                    _logger.LogInformation("Processing message {Message}", message);
                    await _useCase.ExecuteAsync(message);
                    
                    _model.BasicAck(eventArgs.DeliveryTag, false);
                    break;
                }
                catch (Exception e)
                {
                    if (attempt == maxRetryAttempts - 1)
                    {
                        _model.BasicNack(eventArgs.DeliveryTag, false, false);
                        _logger.LogError(e, "Error processing message, ERROR = [{Message}] max retry", e.Message);
                    }
                    else
                    {
                        await Task.Delay(retryDelay);
                        _logger.LogError(e, "Error processing message, ERROR {Message}, Attempt = [{Attempt}]", e.Message, (attempt + 1));
                        
                        retryDelay *= 2;
                    }
                }
            }
        };
        
        _model.BasicConsume(QueueName, false, consumer);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}