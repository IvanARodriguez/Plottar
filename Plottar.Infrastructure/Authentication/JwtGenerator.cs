namespace Plottar.Infrastructure.Authentication;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Plottar.Application.Common.Interfaces.Authentication;

public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
{
  public string GenerateToken(Guid userId, string firstName, string lastName)
  {

    var secret = configuration["JWT_SECRET"] ?? throw new NotImplementedException("No jwt secret found");

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

    var securityToken = new JwtSecurityToken(issuer: "Plottar", claims: claims, signingCredentials: signingCreds, expires: DateTime.Now.AddDays(1));

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
