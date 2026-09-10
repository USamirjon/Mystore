using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Username { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string PasswordHash { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
    [Url]
    public string? ImageUrl { get; set; }
    [Range(0, double.MaxValue)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Wallet { get; set; }
}