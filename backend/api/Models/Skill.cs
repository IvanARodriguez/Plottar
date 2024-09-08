namespace Api.Models;

using System.ComponentModel.DataAnnotations;
using Api.Models.Relationship;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Skill
{
  public Guid Id { get; set; }
  [Required, MinLength(30)]
  public string Name { get; set; } = null!;

  public List<JobSkill> JobSkills { get; set; } = [];
}
