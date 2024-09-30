
namespace Application.User.Queries.Registration;

public class GetUserByIdQuery(Guid userId)
{
  public Guid UserId { get; } = userId;
}
