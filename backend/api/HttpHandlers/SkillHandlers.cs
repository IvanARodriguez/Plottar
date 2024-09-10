

namespace Api.HttpHandlers;

using Api.Interfaces;
using Api.Models.Dtos.Skills;
using ErrorOr;

public static class SkillHandlers
{
  public static void MapSkillsEndpoints(this WebApplication app)
  {
    var endpoint = app.MapGroup("/skills");

    endpoint.MapGet("/", GetAsync);
    endpoint.MapPost("/", CreateAsync);
    endpoint.MapGet("/{id:guid}", GetByIdAsync);

    static async Task<IResult> GetAsync(
      HttpContext ctx,
      ISkillRepository skillRepo
    )
    {
      var skills = await skillRepo.GetSkillsAsync();
      return Results.Ok(skills);
    };

    static async Task<IResult> CreateAsync(
      HttpContext ctx,
      CreateSkillDto createSkillDto,
      ISkillRepository skillRepo
    ) =>
        await skillRepo.CreateSkillAsync(createSkillDto).Match(
          skills => Results.Ok(skills),
          errors => ErrorHandlers.GenerateProblemDetails(ctx, errors)
    );

    static async Task<IResult> GetByIdAsync(
      Guid id,
      HttpContext ctx,
      ISkillRepository skillRepo) =>
     await skillRepo.GetSkillByIdAsync(id).Match(
      skill => Results.Ok(skill),
      errs => ErrorHandlers.GenerateProblemDetails(ctx, errs)
     );

  }
}
