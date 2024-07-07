using Application.DTOs;

namespace Core.Interfaces;

public interface IMailerSendGateway
{
    Task SendMailAsync(SendEmailBody body);
}