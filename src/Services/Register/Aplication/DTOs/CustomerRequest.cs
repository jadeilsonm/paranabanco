namespace Aplication.DTOs;

public class CustomerRequest(
    string name,
    string email,
    string cellNumber,
    string password,
    DateTime dateOfBirth,
    string document,
    Address address)
{
    public string Name { get;  } = name;
    public string Email { get;  } = email;
    public string CellNumber { get;  } = cellNumber;
    public string Password { get;  } = password;
    public DateTime DateOfBirth { get;  } = dateOfBirth;
    public string Document { get;  } = document;
    public Address Address { get; set; } = address;
}

public class Address(string city, string state, string country, string zipCode)
{
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string Country { get; set; } = country;
    public string ZipCode { get; set; } = zipCode;
}