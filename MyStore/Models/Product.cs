using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ProductPrice { get; set; }
    [MaxLength(200)]
    [Required]
    public string ProductDescription { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}