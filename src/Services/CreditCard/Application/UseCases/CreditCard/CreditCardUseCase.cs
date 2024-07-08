using System.Text;
using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreditCard;

public class CreditCardUseCase(IValidator<CreditCardEvent> validator, ILogger<CreditCardEvent> logger, IMailerSendGateway mailerSendGateway)
    : ICreditCardUseCase
{
    private readonly IValidator<CreditCardEvent> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly ILogger<CreditCardEvent> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMailerSendGateway _mailerSendGateway = mailerSendGateway ?? throw new ArgumentNullException(nameof(mailerSendGateway));

    private const char Separator = ' ';
    private const string EmailDomain = "pb@trial-z3m5jgre66mldpyo.mlsender.net";

    public async Task ExecuteAsync(CreditCardEvent creditCardEvent)
    {
        var validationResult = _validator.Validate(creditCardEvent);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation error: {Errors}", validationResult.Errors);
            throw new ValidationException("Validation error: {Errors}", validationResult.Errors);
        }
        
        await SendEmailAsync(creditCardEvent);
    }
    
    private async Task SendEmailAsync(CreditCardEvent creditCardEvent)
    {
        var body = new SendEmailBody()
        {
            From = new Details() { Email = EmailDomain },
            To = new List<Details>() { new Details() { Email = creditCardEvent.Email }},
            Subject = "Credit Card",
            Text = GetMensage(creditCardEvent)
        };
        
        await _mailerSendGateway.SendMailAsync(body);
    }
    
    private string GetMensage(CreditCardEvent creditCardEvent)
    {
        return $"Sr {creditCardEvent.Name}, Your credit card has been approved. Your credit card number is {GetCreditCardNumber()}, your password is {GetCreditCardPassword()}, your expiration date is {GetCreditCardExpirationDate()} and your limit is {GetCreditCardLimit(creditCardEvent.Salary)}";
    }
    
    private string GetCreditCardNumber()
    {
        StringBuilder creditCardNumber = new StringBuilder();
        Random random = new Random();
        for (int i = 0; i < 16; i++)
        {
            if (i % 4 == 0 && i != 0)
            {
                creditCardNumber.Append(Separator);
            }
            creditCardNumber.Append(random.Next(0, 9));
        }
        return creditCardNumber.ToString();
    }
    
    private string GetCreditCardPassword()
    {
        StringBuilder password = new StringBuilder();
        Random random = new Random();
        for (int i = 0; i < 4; i++)
        {
            password.Append(random.Next(0, 9));
        }
        return password.ToString();
    }
    
    private string GetCreditCardExpirationDate()
    {
        var date = DateTime.Now;
        date = date.AddYears(5);
        return date.ToString("MM/yyyy");
    }
    
    private double GetCreditCardLimit(Double salary)
    {
        Random random = new Random();
        return salary * random.Next(10, 100);
    }
}