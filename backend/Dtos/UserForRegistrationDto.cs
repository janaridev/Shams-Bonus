using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record UserForRegistrationDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; init; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Password { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public decimal Bonuses { get; init; } = 0;
}