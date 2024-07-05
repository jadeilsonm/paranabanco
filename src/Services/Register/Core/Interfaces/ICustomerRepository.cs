using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Boolean> IsExistCustomerWithEmail(string email);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> AddCustomerAsync(Customer customer);
}