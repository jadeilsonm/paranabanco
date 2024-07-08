using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("customer")]
public class Customer(
    string name,
    string email,
    string cellNumber,
    string password,
    DateTime dateOfBirth,
    string document,
    double salary,
    double amountAll,
    string city,
    string state,
    string country,
    string zipCode,
    Guid id)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string CellNumber { get; set; } = cellNumber;
    public string Password { get; set; } = password;
    public DateTime DateOfBirth { get; set; } = dateOfBirth;
    public string Document { get; set; } = document;
    public double Salary { get; set; } = salary;
    public double AmountAll { get; set; } = amountAll;
    public Guid Id { get; set; } = id;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string Country { get; set; } = country;
    public string ZipCode { get; set; } = zipCode;
}
    