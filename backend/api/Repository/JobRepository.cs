namespace Api.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Api.Models.Dtos.Job;
using Api.Models.Relationship;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

public class JobRepository(ApplicationDbContext ctx, IMapper map, SkillHelper skillHelper, JobSkillHelper jobSkillHelper) : IJobRepository
{
  private readonly ApplicationDbContext _context = ctx;
  private readonly IMapper _mapper = map;
  private readonly SkillHelper _skillHelper = skillHelper;
  private readonly JobSkillHelper _jobSkillHelper = jobSkillHelper;

  public async Task<ErrorOr<JobDto>> AddAsync(CreateJobDto entity)
  {
    var job = _mapper.Map<Job>(entity);

    if (job is null)
      return Error.Conflict("The job could not be mapped to the request job");

    job.Category = null;

    var allSkills = await _skillHelper
      .GetOrCreateSkillsAsync(entity.Skills);


    job.JobSkills = allSkills
      .Select(s => new JobSkill { JobId = job.Id, SkillId = s.Id })
      .ToList();

    await _context.Jobs.AddAsync(job);
    await _context.SaveChangesAsync();

    return _mapper.Map<JobDto>(job);
  }


  public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
  {
    var jobs = await _context.Jobs
      .Include(c => c.Category)
      .Include(j => j.JobSkills)
      .ThenInclude(js => js.Skill)
      .ToListAsync() ?? [];

    return _mapper.Map<IEnumerable<JobDto>>(jobs);
    ;
  }

  public async Task<ErrorOr<JobDto?>> GetByIdAsync(Guid id)
  {
    var job = await _context.Jobs
      .Include(j => j.Category)
      .Include(j => j.JobSkills)
      .FirstOrDefaultAsync(j => j.Id == id);

    if (job == null)
      return Error.NotFound(description: "The Job was not found");

    return _mapper.Map<JobDto>(job);
  }

  public async Task<ErrorOr<JobDto>> UpdateAsync(Guid id, UpdateJobRequestDto entity)
  {
    var job = await _context.Jobs
        .Include(j => j.JobSkills)
        .ThenInclude(js => js.Skill)
        .FirstOrDefaultAsync(j => j.Id == id);

    if (job is null)
      return Error.NotFound(description: "The job could not be found");
    _mapper.Map(entity, job);

    await _context.SaveChangesAsync();

    if (entity.Skills != null && entity.Skills.Count != 0)
    {
      var allSkills = await _skillHelper.GetOrCreateSkillsAsync(entity.Skills);

      if (allSkills.Count > 0)
      {
        await _jobSkillHelper.UpdateJobSkillsAsync(job, allSkills);
      }
    }
    return _mapper.Map<JobDto>(job);
  }

  public async Task<ErrorOr<int>> DeleteAsync(Guid id)
  {
    var deletedCount = await _context.Jobs
      .Where(job => job.Id == id)
      .ExecuteDeleteAsync();

    if (deletedCount == 0)
      return Error.NotFound(description: "No Job was deleted");

    return deletedCount;
  }

  public async Task<ErrorOr<int>> DeleteJobSkillAsync(Guid id, string skillName)
  {
    var skillToDelete = await _context.Skills
        .Where(s => s.Name == skillName)
        .Select(s => s.Id)
        .SingleOrDefaultAsync();

    if (skillToDelete == Guid.Empty)
    {
      return Error.NotFound(description: $"{skillName} could not be deleted for this job");
    }

    var deletedCount = await _context.JobSkill
        .Where(js => js.JobId == id && js.SkillId == skillToDelete)
        .ExecuteDeleteAsync();

    if (deletedCount == 0)
      return Error.NotFound(description: "Job skill was not deleted");

    return deletedCount;
  }

}
