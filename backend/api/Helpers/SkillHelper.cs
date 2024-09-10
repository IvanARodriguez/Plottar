namespace Api.Helpers;

using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

public class SkillHelper(ApplicationDbContext ctx)
{
  private readonly ApplicationDbContext _context = ctx;
  public async Task<List<Skill>> GetOrCreateSkillsAsync(IEnumerable<string> skillNames)
  {
    var existingSkills = await _context.Skills
          .Where(s => skillNames.Contains(s.Name))
          .ToListAsync();

    var newSkillNames = skillNames
      .Except(existingSkills.Select(s => s.Name))
      .ToList();

    if (newSkillNames.Count > 0)
    {
      var newSkills = newSkillNames.Select(name => new Skill { Name = name }).ToList();
      await _context.Skills.AddRangeAsync(newSkills);
      await _context.SaveChangesAsync();
      existingSkills.AddRange(newSkills);
    }

    return existingSkills;
  }
}
