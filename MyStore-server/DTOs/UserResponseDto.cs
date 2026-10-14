using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.DTOs;

public class UserResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Wallet { get; set; }
}