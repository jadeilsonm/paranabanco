namespace Aplication.DTOs;

public class CustomerResponse(string name, string email, string cellNumber, string password, DateTime dateOfBirth, string document, Address address, Guid id) : CustomerRequest(name, email, cellNumber, password, dateOfBirth, document, address)
{
    public Guid Id { get; } = id;
}