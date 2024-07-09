using Core.Entities;

namespace Core.Interfaces;

public interface ICreditCardRepository
{
    Task<CreditCard?> GetByIdAsync(Guid id);
    Task<IEnumerable<CreditCard>> GetAllAsync();
    Task<CreditCard> AddCustomerAsync(CreditCard creditCard);
}