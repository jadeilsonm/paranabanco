using Core.Entities;

namespace Application.DTOs;

public static class CreditProposalExtensions
{
    public static CreditProposal MapToCreditProposal(OnboardingCustomeEvent onboardingCustomeEvent, double proposalValue)
        => new CreditProposal
        {
            Id = Guid.NewGuid(),
            UserId = onboardingCustomeEvent.Id,
            Name = onboardingCustomeEvent.Name,
            Email = onboardingCustomeEvent.Email,
            ProposalValue = proposalValue,
            CreatedAt = DateTime.UtcNow
        };
}