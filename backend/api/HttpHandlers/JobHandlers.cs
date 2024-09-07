
namespace Api.HttpHandlers;

using Api.Constants;
using Api.Models.Dtos.Job;
using Api.Repository;
using Microsoft.Extensions.Options;

public static class JobHandlers
{
  public static void MapJobEndpoints(this WebApplication app)
  {
    var endpoints = app.MapGroup("/job");

    endpoints.MapGet("/", GetAllJobsAsync);
    endpoints.MapPost("/", CreateJobAsync);
    endpoints.MapGet("/{id:guid}", GetJobByIdAsync);
    endpoints.MapPut("/{id:guid}", UpdateJobAsync);
    endpoints.MapDelete("/{id:guid}", DeleteJobAsync);

    static async Task<IResult> GetAllJobsAsync(IJobRepository jobRepository)
    {
      var jobs = await jobRepository.GetAllJobsAsync();
      return Results.Ok(jobs);
    }

    static async Task<IResult> CreateJobAsync(
     IJobRepository jobRepository,
     CreateJobDto createJobDto
   )
    {
      const int invalidEntityError = StatusCodes.Status422UnprocessableEntity;

      if (!Enum.TryParse<JobUserType>(createJobDto.JobUserType, false, out _))
        return Results.Problem("Invalid user type", statusCode: invalidEntityError);

      if (!Enum.TryParse<SalaryType>(createJobDto.SalaryType, false, out _))
        return Results.Problem("Invalid job status", statusCode: invalidEntityError);

      var options = await jobRepository.AddAsync(createJobDto);

      return options.MatchFirst(
        job => Results.Ok(job),
        err => Results.Problem(err.Description, statusCode: StatusCodes.Status409Conflict)
      );
    }

    static async Task<IResult> GetJobByIdAsync(Guid id, IJobRepository jobRepository)
    {
      var options = await jobRepository.GetByIdAsync(id);

      return options.MatchFirst(
        job => Results.Ok(job),
        err => Results.Problem(err.Description, statusCode: StatusCodes.Status404NotFound)
      );

    }

    static async Task<IResult> UpdateJobAsync(
     Guid id,
     IJobRepository jobRepository,
     UpdateJobRequestDto updateDto)
    {
      var options = await jobRepository.UpdateAsync(id, updateDto);

      return options.MatchFirst(
       job => Results.Ok(job),
       err => Results.Problem(err.Description, statusCode: StatusCodes.Status404NotFound)
     );
    }
    static async Task<IResult> DeleteJobAsync(
     Guid id,
     IJobRepository jobRepository)
    {
      var options = await jobRepository.DeleteAsync(id);
      return options.MatchFirst(
       count => Results.NoContent(),
       err => Results.Problem(err.Description, statusCode: StatusCodes.Status409Conflict)
     );
    }
  }

}
