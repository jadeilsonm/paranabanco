using Application.DTOs;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Proposal;

public class ProposalUseCase(ILogger<ProposalUseCase> logger, IValidator<OnboardingCustomeEvent> validator, IMailerSendGateway mailerSendGateway)
    : IProposalUseCase
{
    private readonly ILogger<ProposalUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IValidator<OnboardingCustomeEvent> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private const string EmailDomain = "pb@trial-z3m5jgre66mldpyo.mlsender.net";

    public async Task ExecuteAsync(OnboardingCustomeEvent onboardingConstomeEvent)
    {
        _logger.LogInformation("Creating proposal for {Name}", onboardingConstomeEvent.Name);
        
        var validationResult = _validator.Validate(onboardingConstomeEvent);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation failed {@Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }
        
        await SendEmailAsync(onboardingConstomeEvent);
        _logger.LogInformation("Finished UseCase {UseCase}", nameof(ProposalUseCase));
    }
    
    private int CalculateScore(DateTime dateOfBirth)
    {
        int age = DateTime.Now.Year - dateOfBirth.Year;
        int score = 0;
        if (age == 18)
        {
            score = 60;
        }
        else if (age > 18 && age < 25)
        {
            score = 70;
        }
        else if (age >= 25 && age < 35)
        {
            score = 80;
        }
        else if (age >= 35 && age < 45)
        {
            score = 90;
        }
        else if (age >= 45)
        {
            score = 100;
        }

        return score;
    }

    private double CalculateAmountProposal(OnboardingCustomeEvent onboardingConstomeEvent)
    {
        int score = CalculateScore(onboardingConstomeEvent.DateOfBirth);
        double amount = onboardingConstomeEvent.Salary * score / 20;
        string document = onboardingConstomeEvent.Document;
        double amountAll = onboardingConstomeEvent.AmountAll;
        if (document.Length > 11)
        {
            amountAll *= 1.5;
        }
        return amount + amountAll;
    }
    
    private string GenerateMsg(OnboardingCustomeEvent onboardingConstomeEvent)
    {
        double amountProposal = CalculateAmountProposal(onboardingConstomeEvent);
        _logger.LogInformation("Proposal created for {Name} with amount {Amount}", onboardingConstomeEvent.Name, amountProposal);
        
        return $"Proposal created for {onboardingConstomeEvent.Name} with amount {amountProposal}";
    }
    
    private async Task SendEmailAsync(OnboardingCustomeEvent onboardingConstomeEvent)
    {
        try
        {
            SendEmailBody body = CreateEmailBody(onboardingConstomeEvent);
            _logger.LogInformation("Sending email to {Email} with message {Message}", onboardingConstomeEvent.Email, body.Text);
            await mailerSendGateway.SendMailAsync(body);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to send email to {Email}", onboardingConstomeEvent.Email);
            throw;
        }
    }
    
    private SendEmailBody CreateEmailBody(OnboardingCustomeEvent onboardingConstomeEvent)
    {
        return new SendEmailBody()
        {
            From = new Details() { Email = EmailDomain },
            To = new List<Details>() { new Details() { Email = onboardingConstomeEvent.Email }},
            Subject = "Proposal credit",
            Text = GenerateMsg(onboardingConstomeEvent)
        };
    }
}