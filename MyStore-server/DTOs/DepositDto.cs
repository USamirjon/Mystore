using System.ComponentModel.DataAnnotations;

namespace MyStore.DTOs;

public class DepositDto
{
    [Required(ErrorMessage = "Сумма обязательна для заполнения")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше нуля")]
    public decimal Amount { get; set; }
}