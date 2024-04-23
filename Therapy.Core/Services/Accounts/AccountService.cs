using System.Security.Claims;
using Therapy.Core.Services.Tokens;
using Therapy.Core.Services.Users;
using Therapy.Domain.DTOs.Account;
using Therapy.Domain.DTOs.User;
using Therapy.Domain.Exceptions;

namespace Therapy.Core.Services.Accounts {
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        
        public Task<UserDTO> CreateAsync(RegisterDTO account)
        { 
          return _userService.AddAsync(account);
        }

        public async Task<AccessToken> LoginAsync(LoginDTO credentials)
        {
          var user = await _userService.GetByEmailAndPasswordAsync(credentials.Email, credentials.Password);
          if (user == null) {
              throw new ValidationException(nameof(user.Email), "Invalid email or password");
          }
          
          var token = _tokenService.GenerateToken(
            new ClaimsIdentity(new Claim[] {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Email, user.Email), 
              new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
              new Claim(ClaimTypes.Role, "Admin")
            }
          ));

          await _userService.UpdateRefreshTokenAsync(user.Id, token.RefreshToken, _tokenService.GetRefreshExpiration());

          return token;
        }

        public async Task<AccessToken> RefreshTokenAsync(string token, string refreshToken)
        {
          var claimsPrincipal =  _tokenService.GetClaimsFromToken(token: token, validateLifetime: false);
          var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
          if (claimsPrincipal == null || id == null) {
              throw new ValidationException(nameof(token), "Invalid token");
          }

          var user = await _userService.GetByIdAndRefreshToken(int.Parse(id.Value), refreshToken);

          if(user == null) {
              throw new NotFoundException("Refresh token");
          }

          if (!user.RefreshToken.IsActive) {
              throw new ValidationException(nameof(user.RefreshToken), "Refresh token has expired.");
          }

          var accessToken = _tokenService.GenerateToken(new ClaimsIdentity(claimsPrincipal.Claims));
          
          await _userService.UpdateRefreshTokenAsync(user.Id, accessToken.RefreshToken, _tokenService.GetRefreshExpiration());

          return accessToken;
        }
    }
}