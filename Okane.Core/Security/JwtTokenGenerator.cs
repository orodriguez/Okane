using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Okane.Core.Entities;

namespace Okane.Core.Security;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(IOptions<JwtSettings> settings) => _settings = settings.Value;

    public string Generate(User user)
    {
        var token = CreateJwtToken(CreateClaims(user), SignCredentials());

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static Claim[] CreateClaims(User user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials) =>
        new(
            issuer: _settings.Issuer, // Replace with your issuer
            audience: _settings.Audience, // Replace with your audience
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes), // Token expiration time
            signingCredentials: credentials
        );

    private SigningCredentials SignCredentials()
    {
        var key = SymmetricSecurityKey();
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return credentials;
    }

    public SymmetricSecurityKey SymmetricSecurityKey() => 
        new(Encoding.ASCII.GetBytes(_settings.SecretKey));
}