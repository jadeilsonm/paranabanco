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
    Guid id)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string CellNumber { get; set; } = cellNumber;
    public string Password { get; set; } = password;
    public DateTime DateOfBirth { get; set; } = dateOfBirth;
    public string Document { get; set; } = document;
    public Guid Id { get; set; } = id;
}
    