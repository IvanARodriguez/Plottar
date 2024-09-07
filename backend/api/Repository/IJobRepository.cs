namespace Api.Repository;

using Api.Models.Dtos.Job;

public interface IJobRepository
{
  Task<IEnumerable<JobDto>> GetAllJobsAsync();
  Task<JobDto?> GetByIdAsync(Guid id);
  Task<JobDto> AddAsync(CreateJobDto entity);
  Task<JobDto> UpdateAsync(Guid id, UpdateJobRequestDto entity);
  Task DeleteAsync(Guid id);
}
