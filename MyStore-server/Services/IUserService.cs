using MyStore.DTOs;
using MyStore.Models;

namespace MyStore.Services;

public interface IUserService
{
    Task<UserResponseDto?> RegisterAsync(UserRegisterDto dto);
    Task<(UserResponseDto User, string Token)?> LoginAsync(UserLoginDto dto);
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<decimal> DepositAsync(int userId, decimal amount);
    Task<decimal> PurchaseAsync(int userId, int productId, int quantity);
    Task<UserResponseDto> UpdateProfileAsync(int userId, string? imageUrl);
}