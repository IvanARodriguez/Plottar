
namespace Api.HttpHandlers;

using Api.Dtos;
using Api.Repository.Common;

public static class JobHandlers
{
  public static void MapJobEndpoints(this WebApplication app)
  {
    var endpoints = app.MapGroup("/api/job");

    endpoints.MapGet("/", async (IRepository<JobDto> jobRepository) =>
    {
      var jobs = await jobRepository.GetAllAsync();
      return Results.Ok(jobs);
    });

    endpoints.MapGet("/{id:guid}", async (
    Guid id,
    IRepository<JobDto> jobRepository) =>
    {
      var job = await jobRepository.GetByIdAsync(id);
      if (job is null)
      {
        return Results.NotFound("The job was not found");
      }
      return Results.Ok(job);
    });
  }

}
