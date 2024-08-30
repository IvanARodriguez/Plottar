namespace Plottar.Domain.Common.Errors;

using ErrorOr;

public static partial class Errors
{
  public static class Authentication
  {
    public static Error InvalidCredentials => Error.Conflict(
       code: "User.InvalidCredential",
       description: "Invalid credentials, please try again");
  }

}
