

namespace Api.HttpHandlers;

using Api.Interfaces;
using Api.Models.Dtos.Skills;
using ErrorOr;

public static class SkillHandlers
{
  public static void MapSkillsEndpoints(this WebApplication app)
  {
    var endpointGroup = app.MapGroup("/skills");

    endpointGroup.MapGet("/", GetSkillsAsync);
    endpointGroup.MapPost("/", CreateSkillAsync);

    static async Task<IResult> GetSkillsAsync(
      HttpContext ctx,
      ISkillRepository skillRepo
    )
    {
      var skills = await skillRepo.GetSkillsAsync();
      return Results.Ok(skills);
    };

    static async Task<IResult> CreateSkillAsync(
      HttpContext ctx,
      CreateSkillDto createSkillDto,
      ISkillRepository skillRepo
    ) =>
        await skillRepo.CreateSkillAsync(createSkillDto).Match(
          skills => Results.Ok(skills),
          errors => ErrorHandlers.GenerateProblemDetails(ctx, errors)
    );
  }
}
