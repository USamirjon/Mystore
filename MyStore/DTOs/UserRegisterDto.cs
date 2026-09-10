using System.ComponentModel.DataAnnotations;

namespace MyStore.DTOs;

public class UserRegisterDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Username { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
    [Url]
    public string? ImageUrl { get; set; }
}