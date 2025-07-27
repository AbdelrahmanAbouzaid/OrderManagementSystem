

using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService(IUnitOfWork unitOfWork, IOptions<JwtOptions> options, IMapper mapper) : IUserService
    {
        public async Task<UserResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await GetUserAsync(loginDto.UserName);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new UnAuthorizedException("Invalid username or password.");

            return new UserResponseDto
            {
                Token = GenerateJwtTokenAsync(user),
                UserName = user.Username,
                Role = user.Role.ToString()
            };

        }

        public async Task<UserResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var userDto = await GetUserAsync(registerDto.UserName);
            if (userDto is not null)
                throw new UserBadRequestException();

            var user = new User
            {
                Username = registerDto.UserName,
                PasswordHash = HashPassword(registerDto.Password),
                Role = UserRole.Customer 
            };

            await unitOfWork.GetRepository<User>().AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return new UserResponseDto
            {
                Token = GenerateJwtTokenAsync(user),
                UserName = user.Username,
                Role = user.Role.ToString()
            };
        }

        public async Task<User?> GetUserAsync(string name)
        {
            var spec = new UserSpecification(name);
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(spec);
            return user;
        }
        private string GenerateJwtTokenAsync(User user)
        {
          
            var jwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return HashPassword(inputPassword) == storedHash;
        }
    }
}
