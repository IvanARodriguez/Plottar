namespace Api.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Repository.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class JobRepository(ApplicationDbContext ctx, IMapper map) : IRepository<JobDto>
{
  private readonly ApplicationDbContext context = ctx;
  private readonly IMapper mapper = map;

  public Task<JobDto> AddAsync(JobDto entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<JobDto>> GetAllAsync()
  {
    var jobs = await this.context.Jobs
      .Include(c => c.Category)
      .Include(j => j.Skills)
      .ToListAsync();

    var mappedJobs = this.mapper.Map<JobDto[]>(jobs);

    return mappedJobs;
  }

  public async Task<JobDto?> GetByIdAsync(Guid id)
  {
    var job = await this.context.Jobs
      .Include(j => j.Category)
      .Include(j => j.Skills)
      .FirstOrDefaultAsync(j => j.Id == id);

    if (job == null)
    {
      return null;
    }

    var jobDto = this.mapper.Map<JobDto>(job);

    return jobDto;
  }

  public Task<JobDto> UpdateAsync(JobDto entity)
  {
    throw new NotImplementedException();
  }
}
