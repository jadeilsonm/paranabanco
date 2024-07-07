using Application.DTOs;

namespace Application.UseCases.Proposal;

public interface IProposalUseCase
{
    Task ExecuteAsync(OnboardingCustomeEvent onboardingConstomeEvent);
}