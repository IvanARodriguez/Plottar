namespace Api.Interfaces;

using Api.Models.Dtos.Job;
using ErrorOr;

public interface IJobRepository
{
  Task<IEnumerable<JobDto>> GetAllJobsAsync();
  Task<ErrorOr<JobDto?>> GetByIdAsync(Guid id);
  Task<ErrorOr<JobDto>> AddAsync(CreateJobDto entity);
  Task<ErrorOr<JobDto>> UpdateAsync(Guid id, UpdateJobRequestDto entity);
  Task<ErrorOr<int>> DeleteAsync(Guid id);
  Task<ErrorOr<int>> DeleteJobSkillAsync(Guid id, string skillName);
}
