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
  private readonly ApplicationDbContext _context = ctx;
  private readonly IMapper _mapper = map;
  public async Task<ErrorOr<SkillDto>> CreateSkillAsync(CreateSkillDto createSkillDto)
  {
    var skill = _mapper.Map<Skill>(createSkillDto);

    if (skill is null)
      return Error.Conflict("Could not map skill");

    // ALL skills must be saved in lowercase
    skill.Name = skill.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture);

    var existingSkill = await _context.Skills
      .FirstOrDefaultAsync(x => x.Name == skill.Name);

    if (existingSkill is not null)
      return Error.Conflict("This skill already exists");

    await _context.Skills.AddAsync(skill);
    await _context.SaveChangesAsync();

    return _mapper.Map<SkillDto>(skill);
  }

  public async Task<ErrorOr<SkillDto?>> GetSkillByIdAsync(Guid id)
  {
    var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Id == id);
    if (skill is null)
      return Error.NotFound("Skill was not found");

    return _mapper.Map<SkillDto>(skill);
  }

  public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
  {
    var skills = await _context.Skills.ToListAsync() ?? [];

    return _mapper.Map<IEnumerable<SkillDto>>(skills);
  }

}
