
namespace Api.Validation;

using Api.Models.Dtos.Skills;
using FluentValidation;

public class CreateSkillRequestValidation : AbstractValidator<CreateSkillDto>
{
  public CreateSkillRequestValidation() => RuleFor(skill => skill.Name).NotNull().NotEmpty();
}
