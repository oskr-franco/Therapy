using Therapy.Domain.DTOs.Account;
using Therapy.Domain.DTOs.User;

namespace Therapy.Core.Services.Accounts {
  public interface IAccountService {
    Task<UserDTO> CreateAsync(RegisterDTO account);
    Task<AccessToken> LoginAsync(LoginDTO credentials);
    Task<AccessToken> RefreshTokenAsync(string token, string refreshToken);
  }
}