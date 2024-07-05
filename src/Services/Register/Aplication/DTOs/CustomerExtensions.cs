using Core.Entities;

namespace Aplication.DTOs;

public static class CustomerExtensions
{
    public static Customer MapToCustomer(CustomerRequest request)
    {
        return new Customer(
            request.Name,
            request.Email,
            request.CellNumber,
            request.Password,
            request.DateOfBirth,
            request.Document,
            Guid.NewGuid()
        );
    }

    public static CustomerResponse MapToCustomerResponse(Customer customer, Address address)
    {
        return new CustomerResponse(customer.Name, customer.Email, customer.CellNumber, customer.Password, customer.DateOfBirth, customer.Document, address, customer.Id);
    }
}