namespace Api.Repository;

using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.Models.Dtos.Skills;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

public class SkillRepository(ApplicationDbContext ctx, IMapper map) : ISkillRepository
{
  private readonly ApplicationDbContext context = ctx;
  private readonly IMapper mapper = map;
  public async Task<ErrorOr<SkillDto>> CreateSkillAsync(CreateSkillDto createSkillDto)
  {
    var skill = this.mapper.Map<Skill>(createSkillDto);

    if (skill is null)
      return Error.Conflict("Could not map skill");

    // ALL skills must be saved in lowercase
    skill.Name = skill.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture);

    var existingSkill = await this.context.Skills
      .FirstOrDefaultAsync(x => x.Name == skill.Name);

    if (existingSkill is not null)
      return Error.Conflict("This skill already exists");

    await this.context.Skills.AddAsync(skill);
    await this.context.SaveChangesAsync();

    return this.mapper.Map<SkillDto>(skill);
  }

  public Task<ErrorOr<SkillDto>> DeleteSkillAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<ErrorOr<SkillDto?>> GetSkillByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
  {
    var skills = await this.context.Skills.ToListAsync() ?? [];

    return this.mapper.Map<IEnumerable<SkillDto>>(skills);
  }

  public Task<ErrorOr<SkillDto>> UpdateSkillAsync(Guid id, SkillDto skillDto)
  {
    throw new NotImplementedException();
  }
}
