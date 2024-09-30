namespace Domain.Entities;

public class User(string email, string password)
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string Email { get; private set; } = email;
  public string Password { get; private set; } = password;
}
