namespace Core.Entities;

public class CreditCard
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double Salary { get; set; }
    public double Limit { get; set; }
    
    public string CardNumber { get; set; }
    public string Password { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}