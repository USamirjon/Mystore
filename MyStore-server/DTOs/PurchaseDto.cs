using System.ComponentModel.DataAnnotations;

namespace MyStore.DTOs;

public class PurchaseDto
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше нуля")]
    public int Quantity { get; set; } = 1; 
}