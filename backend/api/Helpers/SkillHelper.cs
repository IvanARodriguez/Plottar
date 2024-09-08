namespace Api.Helpers;

using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

public class SkillHelper(ApplicationDbContext ctx)
{
  private readonly ApplicationDbContext context = ctx;
  public async Task<List<Skill>> GetOrCreateSkillsAsync(IEnumerable<string> skillNames)
  {
    var existingSkills = await this.context.Skills
          .Where(s => skillNames.Contains(s.Name))
          .ToListAsync();

    var newSkillNames = skillNames
      .Except(existingSkills.Select(s => s.Name))
      .ToList();

    if (newSkillNames.Count != 0)
    {
      var newSkills = newSkillNames.Select(name => new Skill { Name = name }).ToList();
      await this.context.Skills.AddRangeAsync(newSkills);
      await this.context.SaveChangesAsync();
      existingSkills.AddRange(newSkills);
    }

    return existingSkills;
  }
}
