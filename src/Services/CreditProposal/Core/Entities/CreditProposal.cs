namespace Core.Entities;

public class CreditProposal
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double ProposalValue { get; set; }
    public DateTime CreatedAt { get; set; }
}