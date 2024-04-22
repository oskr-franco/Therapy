
using System.Security.Claims;
using Therapy.Domain.DTOs.Account;

namespace Therapy.Core.Services.Tokens
{
    public interface ITokenService
    {
        AccessToken GenerateToken(ClaimsIdentity claims);
        string GenerateRandomToken(int size = 32);
        ClaimsPrincipal GetClaimsFromToken(string token, bool validateLifetime = true);
        DateTime GetRefreshExpiration();
    }
}
