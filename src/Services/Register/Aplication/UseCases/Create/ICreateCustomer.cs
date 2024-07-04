using Aplication.DTOs;

namespace Aplication.UseCases.Create;

public interface ICreateCustomer
{
    public Task<CustomerResponse> ExecuteAsync(CustomerRequest request);
}
