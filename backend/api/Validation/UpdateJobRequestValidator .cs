
namespace Api.Validation;

using Api.Models.Dtos.Job;
using FluentValidation;

public class UpdateJobRequestValidator : AbstractValidator<CreateJobDto>
{
  public UpdateJobRequestValidator()
  {
    RuleFor(job => job.Title)
      .NotNull()
      .NotEmpty()
      .Length(1, 100);

    RuleFor(job => job.Description)
      .NotNull()
      .NotEmpty();

    RuleFor(job => job.ShortDescription)
      .NotNull()
      .NotEmpty()
      .Length(1, 254);

    RuleFor(job => job.CompanyName)
      .NotNull()
      .NotEmpty()
      .Length(1, 100);

    RuleFor(job => job.Salary)
    .GreaterThan(1)
    .LessThanOrEqualTo(100000000.00M)
    .WithMessage("{PropertyName} is too big");

    RuleFor(job => job.CurrencyCode)
    .NotNull()
    .NotEmpty()
    .Length(3);

    RuleFor(job => job.CompanyName)
      .NotNull()
      .NotEmpty()
      .Length(1, 100);

    RuleFor(job => job.JobUserType)
      .NotNull()
      .IsInEnum()
      .WithMessage("{PropertyName} value must be RegisteredUser or AnonymousUser");

    RuleFor(job => job.SalaryType)
      .NotNull()
      .IsInEnum()
      .WithMessage("Salary type value must be Hour, Month, Week, Day, BiWeekly or Year");
  }

}
