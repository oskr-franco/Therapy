using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Core.Extensions;
using Therapy.Domain.DTOs.User;
using Therapy.Domain.Entities;
using Therapy.Domain.Models;
using Therapy.Infrastructure.Repositories;
using Therapy.Domain.Exceptions;
using Therapy.Core.Utils;

namespace Therapy.Core.Services.Users {
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByIdAndRefreshToken(int id, string refreshToken)
        {
            var user = await _userRepository
                                .AsQueryable()
                                .Include(u=> u.RefreshToken)
                                .FirstOrDefaultAsync(e =>
                                    e.Id == id && 
                                    e.RefreshToken.Token == refreshToken
                                );
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.AsQueryable().FirstOrDefaultAsync(e => e.Email == email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmailAndPasswordAsync(string email, string password)
        {
          
          var user = await _userRepository
                            .AsQueryable()
                            .FirstOrDefaultAsync( e => 
                                e.Email == email);
          if (user == null || !Hasher.Verify(user.Password, password)) {
            throw new ValidationException("Password does not match");
          }
          return _mapper.Map<UserDTO>(user);
        }

        public async Task<PaginationResponse<UserDTO>> GetAllAsync(PaginationFilter filter)
        {
            var usersQuery = _userRepository.AsQueryable();
            var earliestDate = _userRepository.AsQueryable().Min(e => (DateTime?)e.CreatedAt);
            var latestDate = _userRepository.AsQueryable().Max(e => (DateTime?)e.CreatedAt);
            var users =
                    await usersQuery
                    .Paginate(
                      filter,
                      (search) => e => e.Email.Contains(search)
                    )
                    .ToPaginationResponse<User, UserDTO>(_mapper, earliestDate, latestDate);
            return users;
        }

        public async Task<UserDTO> AddAsync(UserCreateDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var updatedUser = await _userRepository.AddAsync(user);
            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, UserUpdateDTO user)
        {
            if(id != user.Id) {
                throw new ValidationException("User ID does not match");
            }
            var existingUser = await _userRepository.GetByIdAsync(id, include: x => x.Include(e => e.RefreshToken));
            if (existingUser == null)
            {
                throw new NotFoundException(nameof(Exercise), id);
            }
            var updatedUser = _mapper.Map(user, existingUser);
            await _userRepository.UpdateAsync(updatedUser);
        }

        public Task UpdateRefreshTokenAsync(int id, string refreshToken, DateTime expiration) {
            return UpdateAsync(id, 
                new UserUpdateDTO { 
                    Id = id,
                    RefreshToken = new RefreshToken { 
                        Token = refreshToken,
                        Expires = expiration
                    }});
        }
    }
}