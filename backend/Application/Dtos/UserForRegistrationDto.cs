using System.ComponentModel.DataAnnotations;

namespace backend.Application.Dtos;

public record UserForRegistrationDto
{   
    public string FirstName { get; init; }

    public string LastName { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; init; } // will be phone number

    [Required(ErrorMessage = "Обязательное поле.")]
    [MinLength(7, ErrorMessage = "Пароль должен содержать не менее 7 символов")]
    public string Password { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    [MinLength(7, ErrorMessage = "Пароль должен содержать не менее 7 символов")]
    public string ConfirmPassword { get; init; }

    public decimal Bonuses { get; init; } = 0;
}