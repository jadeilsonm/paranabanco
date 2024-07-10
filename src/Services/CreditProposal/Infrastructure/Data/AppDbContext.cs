using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CreditProposal> CreditProposals { get; init; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditProposal>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<CreditProposal>()
            .Property(x => x.Id)
            .HasConversion(v => v.ToString(),
                v => Guid.Parse(v));
        
        modelBuilder.Entity<CreditProposal>()
            .Property(x => x.UserId)
            .HasConversion(v => v.ToString(),
                v => Guid.Parse(v));
    }
}