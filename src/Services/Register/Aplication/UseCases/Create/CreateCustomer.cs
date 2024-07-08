using Aplication.DTOs;
using Aplication.UseCases.Producer;
using Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Aplication.UseCases.Create;

public class CreateCustomer(IValidator<CustomerRequest> validator, ICustomerRepository customerRepository, IProducerOnboard producerOnboard, ILogger<CreateCustomer> logger)
    : ICreateCustomer
{
    private readonly IValidator<CustomerRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IProducerOnboard _producerOnboard = producerOnboard ?? throw new ArgumentNullException(nameof(producerOnboard));
    private readonly ILogger<CreateCustomer> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        _logger.LogInformation("Iniciando o processo de criação de um novo cliente");
        var validationResult = await _validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            _logger.LogError("Erro de validação ao criar um novo cliente, {@error}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
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
        
        return response;
    }
}