using Therapy.Domain.DTOs.User;
using Therapy.Domain.Entities;
using Therapy.Domain.Models;

namespace Therapy.Core.Services.Users {
  public interface IUserService {
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserDTO> GetByIdAndRefreshToken(int id, string refreshToken);
    Task<UserDTO> GetByEmailAsync(string email);
    Task<UserDTO> GetByEmailAndPasswordAsync(string email, string password);
    Task<PaginationResponse<UserDTO>> GetAllAsync(PaginationFilter filter);
    Task<UserDTO> AddAsync(UserCreateDTO user);
    Task UpdateAsync(int id, UserUpdateDTO user);
    Task UpdateRefreshTokenAsync(int id, string refreshToken, DateTime refreshTokenExpiry);
    Task DeleteAsync(int id);
  }
}