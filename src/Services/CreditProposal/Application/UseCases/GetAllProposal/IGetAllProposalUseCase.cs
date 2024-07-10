using Core.Entities;

namespace Application.UseCases;

public interface IGetAllProposalUseCase
{
    Task<IEnumerable<CreditProposal>> ExecuteAsync();
}