namespace Api.Repository;

using Api.Models.Dtos.Job;

public interface IJobRepository
{
  Task<IEnumerable<JobDto>> GetAllJobsAsync();
  Task<JobDto?> GetByIdAsync(Guid id);
  Task<JobDto> AddAsync(CreateJobDto entity);
  Task<JobDto> UpdateAsync(JobDto entity);
  Task DeleteAsync(Guid id);
}
