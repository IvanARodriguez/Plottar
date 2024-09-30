
namespace Application.User.Interface;

using Application.User.DTOs;

public interface IUserRepository
{
  Task<Guid> CreateUserAsync(string email, string password);
  Task<UserDto> GetUserByIdAsync(Guid userId);
  Task<UserDto> GetUserByEmailAsync(string email);
}

