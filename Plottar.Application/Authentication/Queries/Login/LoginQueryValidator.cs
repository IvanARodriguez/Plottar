
namespace Plottar.Application.Authentication.Queries.Login;

using FluentValidation;
using Plottar.Application.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
  public LoginQueryValidator()
  {
    this.RuleFor(x => x.Email).NotEmpty();
    this.RuleFor(x => x.Password).NotEmpty();
  }
}
