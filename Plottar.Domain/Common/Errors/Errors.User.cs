namespace Plottar.Domain.Common.Errors;

using ErrorOr;

public static partial class Errors
{
  public static class User
  {
    public static Error DuplicateEmail => Error.Conflict(
      code: "User.InvalidCredential",
      description: "Invalid credentials, please try again");
  }
}
