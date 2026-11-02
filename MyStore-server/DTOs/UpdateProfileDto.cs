using System.ComponentModel.DataAnnotations;

namespace MyStore.DTOs;

public class UpdateProfileDto
{
    [Url(ErrorMessage = "Некорректная ссылка на изображение")]
    public string? ImageUrl { get; set; }
}