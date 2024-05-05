using Microsoft.EntityFrameworkCore;
using Plottar_API.Models;

namespace Plottar_API.Data
{
  public class ApplicationDBContext:DbContext
  {
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
    public DbSet<Business> Businesses { get; set; }
    }
}
