using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Therapy.Domain.DTOs.Account;
using Therapy.Domain.Entities;
using Therapy.Domain.Exceptions;

namespace Therapy.Core.Services.Tokens
{
  public class TokenService : ITokenService {
    private readonly TokenSettings _tokenSettings;
    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings.Value;
    }

    public AccessToken GenerateToken(ClaimsIdentity claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AccessToken {
            Token = tokenHandler.WriteToken(token),
            ExpiresIn = _tokenSettings.AccessExpiration,
            RefreshToken = GenerateRandomToken()
        };
    }

    public ClaimsPrincipal GetClaimsFromToken(string token, bool validateLifetime = true)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;

        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = true,
            ValidAudience = _tokenSettings.Audience,
            ValidateIssuer = true,
            ValidIssuer = _tokenSettings.Issuer,
            ValidateLifetime = validateLifetime
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        ClaimsPrincipal claimsPrincipal;
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException ex)
        {
            throw new InternalServerErrorException(ex.Message);
        }
        catch (Exception e)
        {
            throw new InternalServerErrorException(e.Message);
        }
        return claimsPrincipal;
    }

    public string GenerateRandomToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public DateTime GetRefreshExpiration()
    {
        return DateTime.UtcNow.AddMinutes(_tokenSettings.RefreshExpiration);
    }
  }
}
