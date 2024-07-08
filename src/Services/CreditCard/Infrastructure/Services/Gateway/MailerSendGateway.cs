using Core.Configurations;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace Infrastructure.Services.Gateway;

public class MailerSendGateway(IOptions<MailerSendAPIConfiguration> options, ILogger<MailerSendGateway> logger) : IMailerSendGateway
{
    private MailerSendAPIConfiguration _configuration = options.Value;
    private readonly ILogger<MailerSendGateway> _logger = logger;
    
    public async Task SendMailAsync(SendEmailBody body)
    {
        IMailerSendAPI api = Connect();
        try
        {
            _logger.LogInformation("Sending email to {Email}", body.To);
            await api.SendMailAsync(body);
        }
        catch (ApiException e)
        {
            _logger.LogError(e, "Error sending email to {Email}, Error: {Error}", body.To, e.Message);
            throw;
        }
        
    }
    
    private IMailerSendAPI Connect()
    {
        var refitSettings = new RefitSettings()
        {
            AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult(_configuration.Key)
        };
        
        IMailerSendAPI api = RestService.For<IMailerSendAPI>(_configuration.Host, refitSettings);
        return api;
    }
    
}