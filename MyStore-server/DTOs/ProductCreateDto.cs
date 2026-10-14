using System.ComponentModel.DataAnnotations;

namespace MyStore.DTOs;

public class ProductCreateDto
{
    [Required]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [MaxLength(500)]
    public string ProductDescription { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal ProductPrice { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}