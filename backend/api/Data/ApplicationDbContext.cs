namespace Api.Data;

using Api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
  public DbSet<Job> Job { get; set; }
  public DbSet<JobCategory> JobCategory { get; set; }
  public DbSet<Skill> Skill { get; set; }
}
