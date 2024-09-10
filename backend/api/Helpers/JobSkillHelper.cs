

namespace Api.Helpers;

using Api.Data;
using Api.Models;
using Api.Models.Relationship;
using Microsoft.EntityFrameworkCore;

public class JobSkillHelper(ApplicationDbContext context)
{
  private readonly ApplicationDbContext _context = context;

  public async Task UpdateJobSkillsAsync(Job job, IEnumerable<Skill> newSkills)
  {
    // Load existing job skills with their associated Skill entities
    var existingJobSkills = await _context.JobSkill
        .Where(js => js.JobId == job.Id)
        .Include(js => js.Skill)  // Include Skill entity to access Skill.Name
        .ToListAsync();

    // Use HashSets for efficient lookups
    var existingSkillNames = existingJobSkills.Select(js => js.Skill.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
    var newSkillNames = newSkills.Select(s => s.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

    // Identify skills that need to be removed (present in existing but not in new)
    var skillsToRemove = existingJobSkills
        .Where(js => !newSkillNames.Contains(js.Skill.Name, StringComparer.OrdinalIgnoreCase))
        .ToList();

    // Identify skills that need to be added (present in new skills but not in existing)
    var skillsToAdd = newSkills
        .Where(s => !existingSkillNames.Contains(s.Name, StringComparer.OrdinalIgnoreCase))
        .Select(s => new JobSkill { JobId = job.Id, SkillId = s.Id })
        .ToList();

    // Remove outdated JobSkills
    if (skillsToRemove.Count > 0)
    {
      _context.JobSkill.RemoveRange(skillsToRemove);
    }

    // Add new JobSkills
    if (skillsToAdd.Count > 0)
    {
      _context.JobSkill.AddRange(skillsToAdd);
    }

    // Save changes to the database if there are any modifications
    if (skillsToRemove.Count > 0 || skillsToAdd.Count > 0)
    {
      await _context.SaveChangesAsync();
    }
  }

}
