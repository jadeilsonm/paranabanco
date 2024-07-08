namespace Aplication.DTOs;

public class CustomerResponse(string name, string email, string cellNumber, DateTime dateOfBirth, string document, Guid id) 
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string CellNumber { get; } = cellNumber;
    public DateTime DateOfBirth { get; } = dateOfBirth;
    public string Document { get; } = document;
    public Guid Id { get; } = id;

}