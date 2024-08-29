namespace Plottar.Infrastructure.Authentication;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Plottar.Application.Common.Interfaces.Authentication;
using Plottar.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

public class JwtGenerator(
          IConfiguration configuration,
          IDateTimeProvider dateTimeProvider,
          IOptions<JwtSettings> jwtOptions) : IJwtGenerator
{
  private readonly IConfiguration config = configuration;
  private readonly IDateTimeProvider dateProvider = dateTimeProvider;
  private readonly JwtSettings jwtSettings = jwtOptions.Value;

  public string GenerateToken(Guid userId, string firstName, string lastName)
  {
    var secret = this.config["JWT_SECRET"] ?? throw new NotImplementedException("No jwt secret found");

    var signingCreds = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
      SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.GivenName, firstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString())
    };

    var securityToken = new JwtSecurityToken(
      claims: claims,
      issuer: this.jwtSettings.Issuer,
      signingCredentials: signingCreds,
      audience: this.jwtSettings.Audience,
      expires: this.dateProvider.UtcNow.AddMinutes(this.jwtSettings.ExpirationMinutes));

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
