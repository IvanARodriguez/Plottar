namespace Api.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.Models.Dtos.Job;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class JobRepository(ApplicationDbContext ctx, IMapper map) : IJobRepository
{
  private readonly ApplicationDbContext context = ctx;
  private readonly IMapper mapper = map;

  public async Task<JobDto> AddAsync(CreateJobDto entity)
  {
    // Use AutoMapper to map from CreateJobDto to Job entity
    var job = this.mapper.Map<Job>(entity);

    // Make sure to not set navigation properties that may cause unintended inserts
    job.Category = null; // Ensure EF Core does not create a new Category

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
      .ToListAsync();

    var mappedJobs = this.mapper.Map<IEnumerable<JobDto>>(jobs);

    return mappedJobs;
  }

  public async Task<JobDto?> GetByIdAsync(Guid id)
  {
    var job = await this.context.Jobs
      .Include(j => j.Category)
      .Include(j => j.Skills)
      .FirstOrDefaultAsync(j => j.Id == id);

    return job == null ? null : this.mapper.Map<JobDto>(job);
  }

  public Task<JobDto> UpdateAsync(JobDto entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Guid id)
  {
    throw new NotImplementedException();
  }

}
