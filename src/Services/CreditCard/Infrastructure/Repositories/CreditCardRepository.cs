using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardRepository(AppDbContext context) : ICreditCardRepository
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));


    public async Task<CreditCard?> GetByIdAsync(Guid id)
    {
        return await _context.CreditCards.FindAsync(id);
    }

    public async Task<IEnumerable<CreditCard>> GetAllAsync()
    {
        return await _context.CreditCards.ToListAsync();
    }

    public async Task<CreditCard> AddCustomerAsync(CreditCard creditCard)
    {
        await _context.CreditCards.AddAsync(creditCard);
        await _context.SaveChangesAsync();

        return creditCard;
    }
}