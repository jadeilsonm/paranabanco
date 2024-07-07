using Application.DTOs;
using Refit;

namespace Core.Interfaces;

[Headers("accept: application/json", "Content-Type: application/json", "Authorization: Bearer")]
public interface IMailerSendAPI
{
    [Post("/v1/email")]
    Task SendMailAsync([Body] SendEmailBody body);
}