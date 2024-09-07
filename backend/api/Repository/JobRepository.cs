namespace Api.Repository;

using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.Models.Dtos.Job;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

public class JobRepository(ApplicationDbContext ctx, IMapper map) : IJobRepository
{
  private readonly ApplicationDbContext context = ctx;
  private readonly IMapper mapper = map;

  public async Task<ErrorOr<JobDto>> AddAsync(CreateJobDto entity)
  {
    // Use AutoMapper to map from CreateJobDto to Job entity
    var job = this.mapper.Map<Job>(entity);

    if (job is null)
      return Error.Conflict("The job could not be map to the request job");

    // Make sure to not set navigation properties that may cause unintended inserts
    job.Category = null;

    // Add the Job entity to the DbContext
    await this.context.Jobs.AddAsync(job);
    await this.context.SaveChangesAsync();

    // Map the created Job entity back to JobDto to return
    return this.mapper.Map<JobDto>(job);
  }


  public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
  {
    var jobs = await this.context.Jobs
      .Include(c => c.Category)
      .Include(j => j.Skills)
      .ToListAsync() ?? [];

    var mappedJobs = this.mapper.Map<IEnumerable<JobDto>>(jobs);

    return mappedJobs;
  }

  public async Task<ErrorOr<JobDto?>> GetByIdAsync(Guid id)
  {
    var job = await this.context.Jobs
      .Include(j => j.Category)
      .Include(j => j.Skills)
      .FirstOrDefaultAsync(j => j.Id == id);

    if (job == null)
      return Error.NotFound(description: "The Job was not found");

    return this.mapper.Map<JobDto>(job);
  }

  public async Task<ErrorOr<JobDto>> UpdateAsync(Guid id, UpdateJobRequestDto entity)
  {
    var job = await this.context.Jobs.Where(j => j.Id == id).FirstOrDefaultAsync(j => j.Id == id);

    if (job is null)
      return Error.NotFound(description: "The job could not be found");

    this.mapper.Map(entity, job);

    await this.context.SaveChangesAsync();

    return this.mapper.Map<JobDto>(job);
    ;
  }

  public async Task<ErrorOr<int>> DeleteAsync(Guid id)
  {
    var deletedCount = await this.context.Jobs.Where(job => job.Id == id).ExecuteDeleteAsync();
    if (deletedCount == 0)
      return Error.NotFound(description: "No Job was deleted");

    return deletedCount;
  }

}
