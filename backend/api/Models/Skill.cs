namespace Api.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Skill
{
  public Guid Id { get; set; }
  [Required, MinLength(30)]
  public string Name { get; set; } = null!;

  public List<Job> Jobs { get; set; } = [];
}
