using Core.Entities;

namespace Core.Interfaces;

public interface ICreditProposalRepository
{
    Task<CreditProposal?> GetByIdAsync(Guid id);
    Task<IEnumerable<CreditProposal>> GetAllAsync();
    Task<CreditProposal> AddCustomerAsync(CreditProposal creditProposal);
}