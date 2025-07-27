using Shared.DTOs;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResponseDto> LoginAsync(LoginDto loginDto);
    }
}
