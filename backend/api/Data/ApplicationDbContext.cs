namespace Api.Data;

using Api.Models;
using Api.Models.Relationship;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
  public DbSet<Job> Jobs { get; set; }
  public DbSet<JobCategory> JobCategories { get; set; }
  public DbSet<Skill> Skills { get; set; }
  public DbSet<JobSkill> JobSkill { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Job>()
        .HasOne(j => j.Category)
        .WithMany(c => c.Jobs)
        .HasForeignKey(j => j.JobCategoryId)
        .IsRequired();

    // Configure composite primary key for JobSkill
    modelBuilder.Entity<JobSkill>()
        .HasKey(js => new { js.JobId, js.SkillId });

    // Configure relationships
    modelBuilder.Entity<JobSkill>()
        .HasOne(js => js.Job)
        .WithMany(j => j.JobSkills)
        .HasForeignKey(js => js.JobId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<JobSkill>()
        .HasOne(js => js.Skill)
        .WithMany(s => s.JobSkills)
        .HasForeignKey(js => js.SkillId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}
