using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Обязательное поле.")]
    public string Username { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Password { get; init; }
}