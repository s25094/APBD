using Boats.Model;
using Microsoft.EntityFrameworkCore;

namespace Boats.Context;

public class BoatsDbContext : DbContext
{
    public BoatsDbContext()
    {
    }
    
    public BoatsDbContext(DbContextOptions options):base(options) 
    {
        }

    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Sailboat_Reservation> Sailboat_Reservations { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=DbBoats;User Id=sa;Password=Abc?12345;TrustServerCertificate=True");
      
    }
}