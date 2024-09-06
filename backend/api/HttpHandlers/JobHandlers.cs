
namespace Api.HttpHandlers;

using Api.Constants;
using Api.Models.Dtos.Job;
using Api.Repository;

public static class JobHandlers
{
  public static void MapJobEndpoints(this WebApplication app)
  {
    var endpoints = app.MapGroup("/api/job");

    endpoints.MapGet("/", async (IJobRepository jobRepository) =>
    {
      var jobs = await jobRepository.GetAllJobsAsync();
      return Results.Ok(jobs);
    });

    endpoints.MapGet("/{id:guid}", async (
    Guid id,
    IJobRepository jobRepository) =>
    {
      var job = await jobRepository.GetByIdAsync(id);
      if (job is null)
      {
        return Results.Problem("The job was not found", statusCode: StatusCodes.Status404NotFound);
      }
      return Results.Ok(job);
    });
    endpoints.MapPost("/", async (
    CreateJobDto createJobDto,
    IJobRepository jobRepository) =>
    {
      if (!Enum.TryParse<JobUserType>(createJobDto.JobUserType, false, out _))
      {
        return Results.Problem(
          "Invalid user type",
          statusCode: StatusCodes.Status422UnprocessableEntity);
      }
      if (!Enum.TryParse<JobStatus>(createJobDto.Status, false, out _))
      {
        return Results.Problem(
          "Invalid job status",
          statusCode: StatusCodes.Status422UnprocessableEntity);
      }
      if (!Enum.TryParse<SalaryType>(createJobDto.SalaryType, false, out _))
      {
        return Results.Problem(
          "Invalid job status",
          statusCode: StatusCodes.Status422UnprocessableEntity);
      }

      var job = await jobRepository.AddAsync(createJobDto);

      if (job is null)
      {
        return Results.Problem("Job not created", statusCode: StatusCodes.Status409Conflict);
      }
      return Results.Ok(job);
    });
  }

}
