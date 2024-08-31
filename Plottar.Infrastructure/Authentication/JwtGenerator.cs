namespace Plottar.Infrastructure.Authentication;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Plottar.Application.Common.Interfaces.Authentication;
using Plottar.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Plottar.Domain;

public class JwtGenerator(
          IDateTimeProvider dateTimeProvider,
          IOptions<JwtSettings> jwtOptions) : IJwtGenerator
{
  private readonly IDateTimeProvider dateProvider = dateTimeProvider;
  private readonly JwtSettings jwtSettings = jwtOptions.Value;

  public string GenerateToken(User user)
  {
    var secret = this.jwtSettings.Secret ?? throw new NotImplementedException("No jwt secret found");

    var signingCreds = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
      SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
