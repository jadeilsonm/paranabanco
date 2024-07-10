using Core.Entities;

namespace Application.UseCases.GetByIdProposal;

public interface IGetByIdProposal
{
    Task<CreditProposal> ExecuteAsync(Guid id);
}