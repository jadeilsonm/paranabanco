using Core.Entities;

namespace Core.Interfaces;

public interface IMailerSendGateway
{
    Task SendMailAsync(SendEmailBody body);
}