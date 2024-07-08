namespace Application.DTOs;

public class CreditCardEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CellNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string Document { get; set; }
    public Double Salary { get; set; }
    public Double AmountAll { get; set; }
}

public class Address
{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}