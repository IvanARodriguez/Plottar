
namespace Application.User.Events;

using MediatR;

public class UserRegisteredEvent(Guid userId, string email) : INotification
{
  public Guid UserId { get; } = userId;
  public string Email { get; } = email;
}
