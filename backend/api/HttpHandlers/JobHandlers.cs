
namespace Api.HttpHandlers;

using Api.Constants;
using Api.Interfaces;
using Api.Models.Dtos.Job;

public static class JobHandlers
{
  public static void MapJobEndpoints(this WebApplication app)
  {
    var endpoints = app.MapGroup("/jobs");

    endpoints.MapGet("/", GetJobsAsync);
    endpoints.MapPost("/", CreateJobAsync);
    endpoints.MapGet("/{id:guid}", GetJobByIdAsync);
    endpoints.MapPut("/{id:guid}", UpdateJobAsync);
    endpoints.MapDelete("/{id:guid}", DeleteJobAsync);
    endpoints.MapDelete("/{id:guid}/skills/{name}", DeleteJobSkillAsync);

    static async Task<IResult> GetJobsAsync(IJobRepository jobRepository)
    {
      var jobs = await jobRepository.GetAllJobsAsync();
      return Results.Ok(jobs);
    }

    static async Task<IResult> CreateJobAsync(
      HttpContext ctx,
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

      return options.Match(
        job => Results.Ok(job),
        errors => ErrorHandlers.GenerateProblemDetails(ctx, errors)
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
      HttpContext ctx,
      IJobRepository jobRepository,
      UpdateJobRequestDto updateDto)
    {
      var options = await jobRepository.UpdateAsync(id, updateDto);

      return options.Match(
        job => Results.Ok(job),
        errors => ErrorHandlers.GenerateProblemDetails(ctx, errors)
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
    static async Task<IResult> DeleteJobSkillAsync(
     Guid id,
     string name,
     IJobRepository jobRepository)
    {
      var options = await jobRepository.DeleteJobSkillAsync(id, name);
      return options.MatchFirst(
       count => Results.NoContent(),
       err => Results.Problem(err.Description, statusCode: StatusCodes.Status409Conflict)
     );
    }
  }

}
