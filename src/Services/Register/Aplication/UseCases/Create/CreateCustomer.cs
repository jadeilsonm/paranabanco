using Aplication.DTOs;
using Aplication.UseCases.Producer;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Aplication.UseCases.Create;

public class CreateCustomer(IValidator<CustomerRequest> validator, ICustomerRepository customerRepository, IProducerOnboard producerOnboard, ILogger<CreateCustomer> logger, IMailerSendGateway mailerSendGateway)
    : ICreateCustomer
{
    private readonly IValidator<CustomerRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IProducerOnboard _producerOnboard = producerOnboard ?? throw new ArgumentNullException(nameof(producerOnboard));
    private readonly ILogger<CreateCustomer> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMailerSendGateway _mailerSendGateway = mailerSendGateway ?? throw new ArgumentNullException(nameof(mailerSendGateway));
    
    private const string EmailDomain = "pb@trial-z3m5jgre66mldpyo.mlsender.net";

    public async Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        _logger.LogInformation("Iniciando o processo de criação de um novo cliente");
        var validationResult = await _validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            _logger.LogError("Erro de validação ao criar um novo cliente, {@error}", validationResult.Errors);
            throw new DataContractValidationException("Invalid customer data when creating", validationResult.Errors);
        }
        
        Boolean isExistCustomer = await _customerRepository.IsExistCustomerWithEmail(request.Email);
        
        if (isExistCustomer)
        {
            _logger.LogError("Cliente já existe");
            throw new InvalidOperationException("Customer already exists");
        }
        
        var customer = CustomerExtensions.MapToCustomer(request);
        
        await _customerRepository.AddCustomerAsync(customer);

        CustomerResponse response = CustomerExtensions.MapToCustomerResponse(customer);
        
        _logger.LogInformation("Cliente criado com sucesso, enviando para a fila de onboarding");
        _producerOnboard.Send(CustomerExtensions.MapToCustomerEvent(customer), CancellationToken.None);
        
        _logger.LogInformation("Enviando email de boas-vindas para o cliente");
        await SendWelcomeEmail(customer);
        
        return response;
    }
    
    private SendEmailBody GenerateEmailBody(Customer customer)
    {
        return new SendEmailBody()
        {
            From = new Details() { Email = EmailDomain },
            To = new List<Details>() { new() { Email = customer.Email } },
            Subject = "Bem-vindo ao nosso sistema",
            Text = $"Olá {customer.Name}, seja bem-vindo ao nosso sistema"
        };
    }
    
    private Task SendWelcomeEmail(Customer customer)
    {
        SendEmailBody body = GenerateEmailBody(customer);
        return _mailerSendGateway.SendMailAsync(body);
    }
}