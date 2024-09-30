
namespace Application.User.Commands.Register;

using System.Threading;
using System.Threading.Tasks;
using Application.User.Events;
using Application.User.Interface;
using MediatR;


public class RegisterUserCommandHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<RegisterUserCommand, Guid>
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly IMediator _mediator = mediator;

  public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    // Logic for creating a new user
    var userId = await _userRepository.CreateUserAsync(request.Email, request.Password);

    // Raise a user registered event
    await _mediator.Publish(new UserRegisteredEvent(userId, request.Email), cancellationToken);

    return userId;
  }
}
