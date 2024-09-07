
namespace Api.HttpHandlers;

using Api.Constants;
using Api.Models.Dtos.Job;
using Api.Repository;

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

      var job = await jobRepository.AddAsync(createJobDto);

      if (job is null)
        return Results.Problem("Job not created", statusCode: StatusCodes.Status409Conflict);

      return Results.Ok(job);
    }

    static async Task<IResult> GetJobByIdAsync(Guid id, IJobRepository jobRepository)
    {
      var job = await jobRepository.GetByIdAsync(id);
      if (job is null)
      {
        var message = "The job was not found";
        short code = StatusCodes.Status404NotFound;
        return Results.Problem(message, statusCode: code);
      }
      return Results.Ok(job);
    }


    static async Task<IResult> UpdateJobAsync(
     Guid id,
     IJobRepository jobRepository,
     UpdateJobRequestDto updateDto)
    {
      var job = await jobRepository.UpdateAsync(id, updateDto);

      if (job is null)
      {
        var message = "The job was not found";
        short code = StatusCodes.Status404NotFound;
        return Results.Problem(message, statusCode: code);
      }

      return Results.Ok(job);
    }
    static async Task<IResult> DeleteJobAsync(
     Guid id,
     IJobRepository jobRepository)
    {
      await jobRepository.DeleteAsync(id);
      return Results.Ok();
    }
  }

}
