using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CreditCard> CreditCards { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<CreditCard>()
            .Property(x => x.Id)
            .HasConversion(x => x.ToString(), x => Guid.Parse(x));
        
        modelBuilder.Entity<CreditCard>()
            .Property(x => x.CustomerId)
            .HasConversion(x => x.ToString(), x => Guid.Parse(x));
    }
}