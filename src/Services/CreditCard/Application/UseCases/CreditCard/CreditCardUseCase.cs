using System.Text;
using Application.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreditCard;

public class CreditCardUseCase(IValidator<CreditCardEvent> validator, ILogger<CreditCardEvent> logger, IMailerSendGateway mailerSendGateway, ICreditCardRepository repository)
    : ICreditCardUseCase
{
    private readonly IValidator<CreditCardEvent> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly ILogger<CreditCardEvent> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMailerSendGateway _mailerSendGateway = mailerSendGateway ?? throw new ArgumentNullException(nameof(mailerSendGateway));
    private readonly ICreditCardRepository _creditCardRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    
    private const char Separator = ' ';
    private const string EmailDomain = "pb@trial-z3m5jgre66mldpyo.mlsender.net";
    private double _limit = 0;
    private string _password = string.Empty;
    private string _creditCardNumber = string.Empty;
    private string _expirationDate = string.Empty;

    public async Task ExecuteAsync(CreditCardEvent creditCardEvent)
    {
        var validationResult = _validator.Validate(creditCardEvent);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation error: {Errors}", validationResult.Errors);
            throw new DataContractValidationException("Validation error: {Errors}", validationResult.Errors);
        }
        
        SendEmailBody body = GetBody(creditCardEvent);

        var creditCard = creditCardEvent.MapToCreditCard(_limit, _creditCardNumber, _password, _expirationDate);
        
        await SendEmailAsync(body);
        
        _ = await _creditCardRepository.AddCustomerAsync(creditCard);
    }

    private SendEmailBody GetBody(CreditCardEvent creditCardEvent)
    {
        var body = new SendEmailBody()
        {
            From = new Details() { Email = EmailDomain },
            To = new List<Details>() { new Details() { Email = creditCardEvent.Email } },
            Subject = "Credit Card",
            Text = GetMensage(creditCardEvent)
        };
        return body;
    }

    private async Task SendEmailAsync(SendEmailBody body)
    {
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
        _creditCardNumber = creditCardNumber.ToString();
        return _creditCardNumber;
    }
    
    private string GetCreditCardPassword()
    {
        StringBuilder password = new StringBuilder();
        Random random = new Random();
        for (int i = 0; i < 4; i++)
        {
            password.Append(random.Next(0, 9));
        }
        _password = password.ToString();
        return _password;
    }
    
    private string GetCreditCardExpirationDate()
    {
        var date = DateTime.Now;
        date = date.AddYears(5);
        _expirationDate = date.ToString("MM/yyyy");
        return _expirationDate;
    }
    
    private double GetCreditCardLimit(Double salary)
    {
        Random random = new Random();
        _limit = salary * random.Next(10, 100);
        return _limit;
    }
}