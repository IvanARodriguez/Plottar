namespace Api.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class JobCategory
{
  [Required()]
  [Key()]
  public Guid Id { get; set; }

  [Required, MinLength(3), MaxLength(50)]
  public string Name { get; set; } = null!;

  // Navigation Property for One-to-Many Relationship with Jobs
  public List<Job> Jobs { get; set; } = [];
}