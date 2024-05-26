using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Perscription.Model;

namespace Perscription.Context;

public class PerscriptionContext : DbContext
{
    public PerscriptionContext()
    {
    }

    public PerscriptionContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Model.Perscription> Perscriptions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s25094;Integrated Security=True;TrustServerCertificate=True");
    }
}