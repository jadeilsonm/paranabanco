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
            request.Salary,
            request.AmountAll,
            request.Address.City,
            request.Address.State,
            request.Address.Country,
            request.Address.ZipCode,
            Guid.NewGuid()
        );
    }

    public static CustomerResponse MapToCustomerResponse(Customer customer)
    {
        return new CustomerResponse(customer.Name, customer.Email, customer.CellNumber,  customer.DateOfBirth, customer.Document, customer.Id);
    }

    public static CustomerEvent MapToCustomerEvent(Customer customer)
    {
        return new CustomerEvent()
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            CellNumber = customer.CellNumber,
            DateOfBirth = customer.DateOfBirth,
            Document = customer.Document,
            Salary = customer.Salary,
            Address = new Address(customer.City, customer.State, customer.Country, customer.ZipCode),
            AmountAll = customer.AmountAll
        };
    }
}