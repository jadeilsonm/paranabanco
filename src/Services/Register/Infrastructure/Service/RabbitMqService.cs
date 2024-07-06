﻿using Core.Configurations;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Service;

public class RabbitMqService(IOptionsSnapshot<RabbitMqConfiguration> options) : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration = options.Value;
    public IConnection CreateChannel()
    {
        ConnectionFactory connection = new ConnectionFactory()
        {
            UserName = _configuration.UserName,
            Password = _configuration.Password,
            HostName = _configuration.HostName
        };
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
}