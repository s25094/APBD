using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Context;

public class RevenueRecognitionContext : DbContext
{
    public RevenueRecognitionContext()
    {
        
    }

    public RevenueRecognitionContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SoftwareOrder> SoftwareOrders { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<UpfrontContract> UpfrontContracts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=Revenue;User Id=sa;Password=Abc?12345;TrustServerCertificate=true");
    }
}