namespace Plottar.Application.Authentication.Commands.Register;

using FluentValidation;
using Plottar.Application.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
  public RegisterCommandValidator()
  {
    this.RuleFor(x => x.FirstName).NotEmpty();
    this.RuleFor(x => x.LastName).NotEmpty();
    this.RuleFor(x => x.Email).NotEmpty();
    this.RuleFor(x => x.Password).NotEmpty();
  }

}
