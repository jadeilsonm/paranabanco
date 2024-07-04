using Aplication.DTOs;

namespace Aplication.UseCases.Create;

public class CreateCustomer : ICreateCustomer
{
    public Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        throw new NotImplementedException();
    }
}