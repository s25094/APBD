using Microsoft.EntityFrameworkCore;
using Perscription.Model;

namespace Perscription.Context;

public class PharmacyDbContext : DbContext
{
    public PharmacyDbContext()
    {
        
    }

    public PharmacyDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PerscriptionC> PerscriptionCs { get; set; }
    public DbSet<Perscription_Medicament> Perscription_Medicaments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=Pharmacy;User Id=sa;Password=Abc?12345;TrustServerCertificate=True");
      
    }
}