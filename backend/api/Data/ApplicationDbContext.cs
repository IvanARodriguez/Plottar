namespace Api.Data;

using Api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
  public DbSet<Job> Jobs { get; set; }
  public DbSet<JobCategory> JobCategories { get; set; }
  public DbSet<Skill> Skills { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Job>()
        .HasOne(j => j.Category)
        .WithMany(c => c.Jobs)
        .HasForeignKey(j => j.JobCategoryId)
        .IsRequired();
  }
}
