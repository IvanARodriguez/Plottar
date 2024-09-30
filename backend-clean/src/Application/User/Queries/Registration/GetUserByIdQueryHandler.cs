
namespace Application.User.Queries.Registration;

using Application.User.DTOs;
using Application.User.Interface;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
{
  private readonly IUserRepository _userRepository = userRepository;

  public async Task<UserDto> Handle(GetUserByIdQuery request)
  {
    var user = await _userRepository.GetUserByIdAsync(request.UserId);
    return new UserDto { Id = user.Id, Email = user.Email };
  }
}

