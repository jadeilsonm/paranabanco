using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
{
    private readonly AppDbContext appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

    public Task<bool> IsExistCustomerWithEmail(string email)
    {
        return appDbContext.Customers.AnyAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await appDbContext.Customers.AddAsync(customer);
        await appDbContext.SaveChangesAsync();

        return customer;
    }
}