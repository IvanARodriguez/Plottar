namespace Plottar.Infrastructure.Authentication;

public class JwtSettings
{
  public const string SectionName = "JwtSettings";
  public int ExpirationMinutes { get; init; }
  public string Issuer { get; init; } = null!;
  public string Audience { get; init; } = null!;
  public string Secret { get; init; } = null!;
}
