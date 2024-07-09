using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditProposalRepositoryRepository(AppDbContext context)  : ICreditProposalRepository
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<Core.Entities.CreditProposal?> GetByIdAsync(Guid id)
    {
        return await _context.CreditProposals.FindAsync(id);
    }

    public async Task<IEnumerable<Core.Entities.CreditProposal>> GetAllAsync()
    {
       return await _context.CreditProposals.ToListAsync();
    }

    public async Task<Core.Entities.CreditProposal> AddCustomerAsync(Core.Entities.CreditProposal creditProposal)
    {
        await _context.CreditProposals.AddAsync(creditProposal);
        await _context.SaveChangesAsync();
        
        return creditProposal;
    }
}