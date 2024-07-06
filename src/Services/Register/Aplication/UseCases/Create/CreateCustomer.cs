using Aplication.DTOs;
using Aplication.UseCases.Producer;
using Core.Interfaces;
using FluentValidation;

namespace Aplication.UseCases.Create;

public class CreateCustomer(IValidator<CustomerRequest> validator, ICustomerRepository customerRepository, IProducerOnboard producerOnboard)
    : ICreateCustomer
{
    private readonly IValidator<CustomerRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IProducerOnboard _producerOnboard = producerOnboard ?? throw new ArgumentNullException(nameof(producerOnboard));

    public async Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Console.WriteLine(request.Email);

        Boolean isExistCustomer = await _customerRepository.IsExistCustomerWithEmail(request.Email);
        
        Console.WriteLine(isExistCustomer);
        
        if (isExistCustomer)
        {
            throw new InvalidOperationException("Customer already exists");
        }
        
        var customer = CustomerExtensions.MapToCustomer(request);
        
        await _customerRepository.AddCustomerAsync(customer);

        CustomerResponse response = CustomerExtensions.MapToCustomerResponse(customer, request.Address);
        
        _producerOnboard.Send(response, CancellationToken.None);
        
        return response;
    }
}