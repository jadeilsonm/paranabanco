using Core.Entities;

namespace Application.UseCases.GetByIdProposal;

public interface IGetByIdProposalUseCase
{
    Task<CreditProposal> ExecuteAsync(Guid id);
}