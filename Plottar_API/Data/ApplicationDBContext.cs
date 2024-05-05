using Microsoft.EntityFrameworkCore;
using Plottar_API.Models;

namespace Plottar_API.Data
{
  public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
  {
    public DbSet<Business> Businesses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasPostgresExtension("uuid-ossp");

      modelBuilder.Entity<Business>().HasData(
        new Business(){
          Name = "Hitab",
          Address = "3801 Vitruvian Way",
          City = "Addison",
          State = "Texas",
          Country = "United States",
          PostalCode="38001"
        }
        );
    }
  }
}
