using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record UserForRegistrationDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; init; } // will be phone number

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Password { get; init; }

    public decimal Bonuses { get; init; } = 0;
}