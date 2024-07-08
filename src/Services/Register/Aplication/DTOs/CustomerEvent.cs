namespace Aplication.DTOs;

public class CustomerEvent
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
