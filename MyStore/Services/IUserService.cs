using MyStore.DTOs;
using MyStore.Models;

namespace MyStore.Services;

public interface IUserService
{
    Task<UserResponseDto?> RegisterAsync(UserRegisterDto dto);
    Task<UserResponseDto?> LoginAsync(UserLoginDto dto);
    Task<UserResponseDto?> GetByIdAsync(int id);
}