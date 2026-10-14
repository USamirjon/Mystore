using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.DTOs;

public class UserLoginDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Login { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}